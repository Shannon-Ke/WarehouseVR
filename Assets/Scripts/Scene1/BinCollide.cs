using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollide : MonoBehaviour {
    public GameObject events;
    Toggle toggle;
 
    //problem is that this keeps getting called every second i guess
    private void Start()
    {
        toggle = events.GetComponent<Toggle>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ReferenceEquals(collision.collider.gameObject, ControllerGrabObject.oldObject))
            
        {
            ControllerGrabObject.oldObject.transform.parent = transform;
            toggle.IncrementBin();
            ControllerGrabObject.oldObject = null;
        }
    }
    
}
