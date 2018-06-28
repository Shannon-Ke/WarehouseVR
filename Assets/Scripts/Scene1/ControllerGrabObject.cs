using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

 public class ControllerGrabObject : MonoBehaviour {
    public static ControllerGrabObject control;
    //anchor the cart to the user if they pick it up
    public static Vector3 origPos;
    public static Quaternion originalRotationValue;
    public static Vector3 bin1pos;
    public static Quaternion bin1rot;
    public static Vector3 bin2pos;
    public static Quaternion bin2rot;
    public GameObject bin1;
    public GameObject bin2;
    public GameObject cart;
    public GameObject camrig;
    public GameObject held;
    public static bool cartgrabbed = false;
    bool grabcart = false;
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
 	public GameObject GetHeld()
    {
        return objectInHand;
    }
 	void Awake()
 	{
        bin1pos = bin1.transform.position;
        bin2pos = bin2.transform.position;
        bin1rot = bin1.transform.rotation;
        bin2rot = bin2.transform.rotation;
        originalRotationValue = cart.transform.rotation;
        origPos = cart.transform.position;
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
        objectInHand.GetComponent<Collider>().enabled = false;
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
            objectInHand.GetComponent<Collider>().enabled = true;
 	    }
 	    // Remove the reference to the formerly attached object.
 	    objectInHand = null;
 	}
    // Update is called once per frame
    void Update()
    {
        // When the player squeezes the trigger and there’s a potential grab target, this grabs it.
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                if (!grabcart && collidingObject.name == "pCube741")
                {
                    //change this to the camera rig
                    cart.transform.parent = camrig.transform;
                    grabcart = true;
                    held.SetActive(true);
                    
                    foreach (Collider c in  collidingObject.GetComponents<Collider>()) { c.isTrigger = true; }
                    foreach (Collider c in collidingObject.GetComponentsInChildren<Collider>()) { c.isTrigger = true; }
                    collidingObject.GetComponent<Rigidbody>().isKinematic = true;
                    foreach (Rigidbody r in collidingObject.GetComponentsInChildren<Rigidbody>()) { r.isKinematic = true; }
                    cartgrabbed = true;
                }
                else if (grabcart && collidingObject.name == "pCube741")
                {
                    bin1pos = bin1.transform.position;
                    bin2pos = bin2.transform.position;
                    origPos = cart.transform.position;
                    cart.transform.parent = null;
                    grabcart = false;
                    held.SetActive(false);
                    foreach (Collider c in collidingObject.GetComponents<Collider>()) { c.isTrigger = false; }
                    foreach (Collider c in collidingObject.GetComponentsInChildren<Collider>()) { c.isTrigger = false; }
                    collidingObject.GetComponent<Rigidbody>().isKinematic = false;
                    foreach (Rigidbody r in collidingObject.GetComponentsInChildren<Rigidbody>()) { r.isKinematic = false; }
                    cartgrabbed = false;
                }
                else { GrabObject(); }

            }
        }

        // If the player releases the trigger and there’s an object attached to the controller, this releases it.
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }

        }
    }
 	    public GameObject GetObjectInHand() {
 		    return objectInHand;
 	    }
 }
