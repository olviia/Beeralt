
using Actor;
using Helpers;
using UnityEngine;

namespace PhysicsElement
{
    //lives on a rain drop geometry 
    //has to have SphereCollider
    public class RainDrop:MonoBehaviour
    {
        //get the collider 
        private SphereCollider sphereCollider;
        private bool isAttached = false;
        [SerializeField] private float _mass = 0.1f;
        
        [SerializeField] private float livingTime = 5f;
        private float currentTime;
        
        private Rigidbody affectedRigidbody;
        private ShiftableGeometry shiftable;
        private IGameActor actor;

        private void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            if (currentTime > livingTime)
            {
                Destroy(gameObject);
            }
            else if(!isAttached)
            {
                currentTime += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isAttached) return;
            
            affectedRigidbody = other.attachedRigidbody;
            
            actor = affectedRigidbody?.GetComponent<IGameActor>();

            if (actor != null)
            {
                shiftable = affectedRigidbody.GetComponentInChildren<ShiftableGeometry>();

                //transform the position of a drop in the local space of a player
                Vector3 localDropPosition = shiftable.transform.parent.InverseTransformPoint(transform.position);
                Vector3 shiftDirection = localDropPosition.normalized;

                //move by half a radius
                var magnitude = sphereCollider.radius * 0.5f;
                shiftable.Shift(shiftDirection * magnitude);
                
                //add the mass
                affectedRigidbody.mass += _mass;
                
                //attach here
                isAttached = true;
                transform.SetParent(affectedRigidbody.transform);
                sphereCollider.isTrigger = false;
                Destroy(GetComponent<Rigidbody>());
                
                //subscribe on camera shaking event
                //add method Detouch, reset position and mass
                actor.OnCameraShaking += Deattach;
            }
        }

        private void Deattach()
        {
            affectedRigidbody.mass -= _mass;
            //there should be null check if actor is null. while it shouldn't be the case, but just in case
            if(actor != null) actor.OnCameraShaking -= Deattach;
            affectedRigidbody.GetComponentInChildren<ShiftableGeometry>().Reset();
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().useGravity = true;
            isAttached = false;
            transform.SetParent(null);
            
        }

    }
}