using System;
using Content.ObjectsThatExistOnScene;
using UnityEngine;

namespace Modes.EditMode.States
{
    public class ObjectMoveState:IPlacementState
    {
        private PlacedObjectMarker marker;
        private GameObject colliderlessMarker;

        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Vector3 originalScale;
        
        public event Action OnFinishedMove;
        
        public void OnEnter()
        {
            originalPosition = marker.placedInstance.position;
            originalRotation = marker.placedInstance.rotation;
            originalScale = marker.placedInstance.scale;
            
            colliderlessMarker = GameObject.Instantiate(marker.gameObject, originalPosition, originalRotation);
            colliderlessMarker.transform.localScale = originalScale;
            foreach (var collider in colliderlessMarker.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            marker.gameObject.SetActive(false);
        }

        public void OnExit()
        {
            marker.gameObject.SetActive(true);
            GameObject.Destroy(colliderlessMarker);
        }

        public void Update(RaycastHit hit, bool isHit)
        {
            if(isHit) colliderlessMarker.transform.position = hit.point;
        }

        public void PlaceMove()
        {
            //place the object, update it's data in PlacedInstance
            marker.gameObject.transform.position = colliderlessMarker.transform.position;
            marker.gameObject.transform.rotation = colliderlessMarker.transform.rotation;
            marker.gameObject.transform.localScale = colliderlessMarker.transform.localScale;
            marker.Place(marker.gameObject);
            
            OnFinishedMove?.Invoke();
        }

        public void Scroll(int direction)
        {
            //do nothing
        }

        public void Inspect()
        {
            marker.gameObject.transform.position = originalPosition;
            marker.gameObject.transform.rotation = originalRotation;
            marker.gameObject.transform.localScale = originalScale;
            marker.Place(marker.gameObject);
            
            OnFinishedMove?.Invoke();
        }

        public void Prepare(PlacedObjectMarker marker)
        {
            this.marker = marker;
        }
    }
}