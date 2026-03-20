using System;
using Content.ObjectsThatExistOnScene;
using UnityEngine;

namespace Modes.EditMode.States
{
    public class NothingSelectedState:IPlacementState
        
    {
        private IEditModeHost host;
        private PlacedObjectMarker objectMarker;
        public event Action OnScroll;
        public event Action<PlacedObjectMarker> OnObjectMoveRequested;
        public event Action<PlacedObjectMarker> OnObjectSelected;
        public void OnEnter()
        {
            //do nothing
            Debug.Log("Entered NothingSelectedState");
        }

        public void OnExit()
        {
            //return the highlight on hit object to original shader
        }

        public void Update(RaycastHit hit, bool isHit)
        {
            if (!isHit) objectMarker = null;
            else objectMarker = hit.collider?.GetComponent<PlacedObjectMarker>();
        }

        public void PlaceMove()
        {
            if (objectMarker != null)
            {
                OnObjectMoveRequested?.Invoke(objectMarker);
            }
        }

        public void Scroll(int direction)
        {
            OnScroll?.Invoke();
        }

        public void Inspect()
        {
            if (objectMarker != null)
            {
                OnObjectSelected?.Invoke(objectMarker);
            }
        }
    }
}