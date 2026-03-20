using System.Collections.Generic;
using Commands;
using Helpers;
using Input.Contexts;
using UnityEngine;

namespace Input
{
    //in this script, i select what input source the app is listening to
    public class InputManager: MonoBehaviour
    {
        public List<IInputSource> inputSources = new ();
        private IInputSource activeSource;
        //this is so we can go between contexts
        private Stack<IInputContext> contexts = new ();
        
        //empty lists for the input not related to the context
        private readonly List<IMovementCommand> emptyMovementList = new();
        private readonly List<IActionCommand> emptyActionList = new();
        private readonly List<IMetaCommand> emptyMetaList = new();
        private readonly List<IPlacementCommand> emptyPlacementList = new();

        void Start()
        {
            PlatformType platform = DeviceDetector.GetCurrentPlatform();
            CreateInputSourcesForPlatform(platform);
            
            contexts.Push(new PlayContext());

            foreach (var source in inputSources)
            {
                source.OnBecameActive += SwitchToSource;

            }
            activeSource = inputSources[0];
        }

        private void CreateInputSourcesForPlatform(PlatformType platform)
        {
            switch (platform)
            {
                case PlatformType.Desktop:
                    inputSources.Add(new KeyboardInputSource());
                    //inputSources.Add(new GamepadInputSource());
                    break;
                case PlatformType.Mobile:
                    //inputSources.Add(new TouchInputSource()); 
                    break;
                case PlatformType.DesktopVR:
                case PlatformType.AndroidVR:
                    // inputSources.Add(new VRInputSource()); 
                    // inputSources.Add(new KeyboardInputSource()); 
                    break;
            }
        }

        private void SwitchToSource(IInputSource newSource)
        {
            activeSource = newSource;
        }

        public List<IMovementCommand> GetMovementInput()
        {
            if (contexts.Peek().IsMovementActive) return activeSource?.GetMovementInput();
            return emptyMovementList;
        }
        public List<IActionCommand> GetActionInput()
        {
            if (contexts.Peek().IsActionActive) return activeSource?.GetActionInput();
            return emptyActionList;
            
        }
        
        public List<IMetaCommand> GetMetaInput()
        {
            if (contexts.Peek().IsMetaActive) return activeSource?.GetMetaInput();
            return emptyMetaList;
        }

        public List<IPlacementCommand> GetPlacementInput()
        {
            if (contexts.Peek().IsPlacementActive) return activeSource?.GetPlacementInput();
            return emptyPlacementList;
        }

        public Vector2 GetCameraRotation()
        {
            if (!contexts.Peek().IsMovementActive) return Vector2.zero;
            return activeSource?.GetRotation() ?? Vector2.zero;
        }
        
        //add context on top of stack
        public void PushContext(IInputContext context) =>  contexts.Push(context);
        //remove the context from top of stack
        public void PopContext() => contexts.Pop();
        //check context
        public IInputContext PeekContext() => contexts.Peek();

        public void ResetCursor()
        {
            activeSource?.ResetCursor();
        }
    
    }
}
