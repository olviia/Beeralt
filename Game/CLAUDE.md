# Beeralt — Project Context for Claude

## How we work together

- Claude does NOT write code into project files.
- Claude helps by: explaining, guiding implementation step by step, tracing dependencies, finding examples, flagging architectural concerns.
- Always read relevant files before advising — context-blind advice is not acceptable.
- Treat the user as an intelligent equal. No hand-holding unless asked.
- When the user shares their reasoning, engage with it rather than starting from scratch.
- Clean, scalable architecture is a top priority. Flag when suggestions might compromise it.

## About the project

A Unity game (C#) where the player is a bee. Ambitious RPG with an in-game edit mode (place/move objects in the world). Targets Desktop, Mobile, VR (Meta Quest Android).

The user is learning programming through building this game. Architecture clarity is as important as features.

## Architecture at a glance

| Pattern | Where |
|---|---|
| Command | Input → `IMovementCommand`, `IActionCommand`, `IMetaCommand`, `IPlacementCommand` |
| Strategy | Movement → `FlyingStrategy`, `WalkingStrategy`, `KinematicMovementStrategy` |
| Context stack | `InputManager` holds `Stack<IInputContext>` — filters active command categories |
| State machine | EditMode placement → `NothingSelectedState`, `GhostPlacingState`, `ObjectMoveState` |
| Mediator | `QuestMediator` — connects inventory, location events, quest objectives, UI |
| Visitor | `ILocationVisitor` on `BeeGameActor` |
| Observer (events) | Used broadly via C# `Action` events |

**Key classes:**
- `BeeGameActor` — player root, implements `IGameActor` + `IInventoryHolder` + `ILocationVisitor`
- `InputManager` — platform-aware source selection, context stack
- `MovementModeController` — owns strategy switching and landing detection
- `EditModeController` — implements `IMenu` + `IEditModeHost`, notifies `IEditModeParticipant` list
- `QuestMediator` — quest lifecycle, completion checks, UI event bus
- `RegistryController` / `PrefabRegistry` — placeable content registry
- `SaveSystem` — persistence

**Known stubs:** `Dash()` and `Attack()` are `NotImplementedException`.

**Known edge case (noted in code):** `QuestMediator.StartQuest` assumes player starts with empty inventory and not inside any trigger location. Needs addressing when save state is implemented.

---

## Active investigation — WalkingStrategy

### Goal
Multi-surface walking (floors, walls, ceilings) that stops at edges. The bee lands on a surface and walks on it relative to the surface normal.

### Current approach
`WalkingStrategy` uses `AddForce` / `AddRelativeForce` with `useGravity = false`.
Passive force (`GetPassiveForce`) pushes the bee INTO the surface (`-gravityMagnitude * surfaceNormal`).
Rotation aligns to surface normal via `Quaternion.FromToRotation`.

### Bugs identified in current code

1. **`CheckDirection` ignores its parameter** — the ray is always cast in `-myNormal` (down from head) regardless of which direction was passed. So edge detection ahead is not actually direction-aware.

2. **Inconsistency in force application** — `MoveForward` uses `myForward` (world-space, surface-aligned). `MoveBackward/Left/Right` use `AddRelativeForce` with local-space vectors. These are different reference frames and will not behave symmetrically.

3. **`targetRotation` computed in `CheckDirection` but never applied to the rigidbody** — `CheckDirection` calculates a `targetRotation` but never uses it to rotate the object. The actual surface-aligning rotation only happens in `OnEnter`.

4. **`distGround` is computed in `OnEnter` but never used anywhere.**

5. **`Rotate()` fights itself** — lerps toward `targetRot` (surface alignment), then directly multiplies in a yaw delta. The lerp and direct assignment fight each other each frame.

6. **No edge detection** — no check for "no surface ahead", so the bee walks off edges without transitioning back to flying.

### Approaches used by others — for reference

**Shared core across all reliable surface walkers:**
- Raycast downward every FixedUpdate (in `-transform.up`, character's "down")
- If hit: update surface normal, apply adhesion force, rotate body to align
- If NOT hit: handle the "no surface" case (stop, transition, etc.)

**Catlike Coding "Custom Gravity" series** — search "Catlike Coding custom gravity unity"
Rigorous treatment of ground snapping, slopes, surface adhesion. Uses direct velocity instead of AddForce.

**Spider / gecko style demos** — search "Unity surface walk normal alignment" or "unity spider walk rigidbody"
Some use `Rigidbody.MovePosition` (kinematic), some set velocity directly. All use the raycast-every-frame grounding pattern.

**Alternative to AddForce:** setting `rigidbody.linearVelocity` directly gives immediate response and natural stopping. AddForce accumulates — requires drag tuning to feel right.

### Current decision
Keep AddForce. Immediate goal: when the raycast finds no surface below, stop the bee (zero velocity) rather than transitioning to flying.