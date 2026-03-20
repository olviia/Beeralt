using UnityEngine;
using PhysicsElement;
using System.ComponentModel;

public class GroundPatch : MonoBehaviour
 {
    [SerializeField] GameObject whatToGrow;

    //in the future it is better to check the property of the object, if is water,
    //then grow something
    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.GetComponent<RainDrop>() != null)
        {            
            Water();
            Destroy(collision.gameObject);
        }
    }
    public void OnTriggerEnter(Collider collider){
        if(collider.gameObject.GetComponent<RainDrop>() != null)
        {            
            Water();
            Destroy(collider.gameObject);
        }
    }
    private void Water(){

        Instantiate(whatToGrow, transform.position, transform.rotation);
    }
}
