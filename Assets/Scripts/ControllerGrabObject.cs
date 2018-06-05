using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

 public class ControllerGrabObject : MonoBehaviour {

    //anchor the cart to the user if they pick it up

    public GameObject cart;
 	private SteamVR_TrackedObject trackedObj;
 	// 1 Stores the GameObject that the trigger is currently colliding with,
 	// so you have the ability to grab the object.
 	private GameObject collidingObject; 
 	// 2 Serves as a reference to the GameObject that the player is currently grabbing.
 	private GameObject objectInHand;
    public static GameObject oldObject;
 	private SteamVR_Controller.Device Controller
        
 	{
 	    get { return SteamVR_Controller.Input((int)trackedObj.index); }
 	}
 	
 	void Awake()
 	{
       
 	    trackedObj = GetComponent<SteamVR_TrackedObject>();
 	}
 	private void SetCollidingObject(Collider col)
 	{
 	    // 1 Doesn’t make the GameObject a potential grab target if the player is
 	    // already holding something or the object has no rigidbody.
 	    if (collidingObject || !col.GetComponent<Rigidbody>())
 	    {
 	        return;
 	    }
 	    // 2 assigns object as potential grab target
 	    collidingObject = col.gameObject;
       
 	}

 	/******* TRIGGER METHODS *******/

 	// when trigger collider enters another collider, this sets up the other collider as 
 	// a potential grab target
 	public void OnTriggerEnter(Collider other)
 	{
        SetCollidingObject(other);
    }

 	// ensures target is set when player holds controller over an object for a while
 	public void OnTriggerStay(Collider other)
 	{
 	    SetCollidingObject(other);
 	}

 	// collider exits an object, abandoning ungrabbed target, code removes target
 	// by setting it to null
 	public void OnTriggerExit(Collider other)
 	{
 	    if (!collidingObject)
 	    {
 	        return;
 	    }

 	    collidingObject = null;
 	}

 	/********GRAB METHODS********/

 	private void GrabObject()
 	{
 	    // Move the GameObject inside the player’s hand and remove it from the collidingObject variable.
 	    objectInHand = collidingObject;
        oldObject = objectInHand;
 	    collidingObject = null;
 	    // Add a new joint that connects the controller to the object using the AddFixedJoint() method below.
 	    var joint = AddFixedJoint();
 	    joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
 	}

 	// Make a new fixed joint, add it to the controller, and then set it up so it
 	// doesn’t break easily. Finally, you return it.
 	private FixedJoint AddFixedJoint()
 	{
 	    FixedJoint fx = gameObject.AddComponent<FixedJoint>();
 	    fx.breakForce = 20000;
 	    fx.breakTorque = 20000;
 	    return fx;
 	}

 	/********RELEASE METHOD********/

 	private void ReleaseObject()
 	{
 	    // Make sure there’s a fixed joint attached to the controller.
 	    if (GetComponent<FixedJoint>())
 	    {
 	        // Remove the connection to the object held by the joint and destroy the joint.
 	        GetComponent<FixedJoint>().connectedBody = null;
 	        Destroy(GetComponent<FixedJoint>());
 	        // Add the speed and rotation of the controller when the player releases the
 	        // object, so the result is a realistic arc
 	        objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
 	        objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
 	    }
 	    // Remove the reference to the formerly attached object.
 	    objectInHand = null;
 	}
 	// Update is called once per frame
 	void Update () {
 		// When the player squeezes the trigger and there’s a potential grab target, this grabs it.
 		if (Controller.GetHairTriggerDown())
 		{
 		    if (collidingObject)
 		    {
                if (collidingObject.name == "pCube741")
                {
                    //anchor to main cam
                    //cart.transform.position = new Vector3(transform.position.x, -0.015f, transform.position.z + 0.08f);
                    cart.transform.parent = transform;

                } else { GrabObject(); }
               
 		    }
 		}

 		// If the player releases the trigger and there’s an object attached to the controller, this releases it.
 		if (Controller.GetHairTriggerUp())
 		{
 		    if (objectInHand)
 		    {
 		        ReleaseObject();
 		    } else if (collidingObject && collidingObject.name == "pCube741")
            {
                cart.transform.parent = null;
            }
 		}
 		
 	}
 	public GameObject GetObjectInHand() {
 		return objectInHand;
 	}
 }
