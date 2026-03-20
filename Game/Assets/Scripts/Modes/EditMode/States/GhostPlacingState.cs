using System;
using Content.ObjectsThatExistOnScene;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modes.EditMode.States
{
    public class GhostPlacingState:IPlacementState
    {
        private IContentNavigator contentNavigator;
        private EditModeDisplaySelectionMenu displaySelectionMenu;
        private GameObject ghostPrefab;

        private RaycastHit hit;
        private bool isHit;

        public event Action<PlacedInstance> OnInstancePlaced;
        public event Action OnCancelled;

        public GhostPlacingState(IContentNavigator contentNavigator,EditModeDisplaySelectionMenu displaySelectionMenu)
        {
            this.contentNavigator = contentNavigator;
            this.displaySelectionMenu = displaySelectionMenu;
        }
        
        public void OnEnter()
        {
            contentNavigator.OnChanged += RefreshGhost;
            InstantiateGhost();
            displaySelectionMenu.Open(contentNavigator);
            Debug.Log("Ghost Placing State");
        }

        public void OnExit()
        {
            DestroyGhost();
            contentNavigator.OnChanged -= RefreshGhost;
            displaySelectionMenu.Close();
        }

        public void Update(RaycastHit hit, bool isHit)
        {
            this.hit = hit;
            this.isHit = isHit;
            
            if (isHit)
            {
                ghostPrefab.SetActive(true);
                ghostPrefab.transform.position = hit.point;
            }
            else
            {
                ghostPrefab.SetActive(false);
            }
        }

        public void PlaceMove()
        {
            if(!isHit) return;

            GameObject newPrefab = Object.Instantiate(contentNavigator.CurrentPrefab, hit.point, Quaternion.identity);
            PlacedObjectMarker placedObjectMarker = newPrefab.AddComponent<PlacedObjectMarker>();
            //here put the information about the placed prefab and also the prefab id from contentNavigator
            //as we can have lots of object with the same prefab id
            placedObjectMarker.Initialize(newPrefab, contentNavigator.CurrentPrefabID);
            
            //and here we ask Placement Controller to put a new prefab into PlacedData
            OnInstancePlaced?.Invoke(placedObjectMarker.placedInstance);
        }

        public void Scroll(int direction)
        {
            //hah so many passes through...
            contentNavigator.Scroll(direction);
        }

        public void Inspect()
        {
            OnCancelled?.Invoke();
        }

        private void InstantiateGhost()
        {
            var selectedPrefab = contentNavigator.CurrentPrefab;
            ghostPrefab = Object.Instantiate(selectedPrefab);
            
            //disable all physics so it doesn't interact with anything
            foreach (var collider in ghostPrefab.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            var rb = ghostPrefab.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }

        private void DestroyGhost()
        {
            //i don't know why but in unity the ghost is not destroyed when i exit edit mode
            if (ghostPrefab != null)
            {
                Object.Destroy(ghostPrefab);
                ghostPrefab = null;
            }
        }

        private void RefreshGhost()
        {
            DestroyGhost();
            InstantiateGhost();
        }
    }
}