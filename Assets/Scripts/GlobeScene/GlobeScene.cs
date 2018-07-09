using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class GlobeScene : MonoBehaviour
{
    bool info = true;
    public GameObject countryinfo, laserRays;
    public GameObject viewhighlight;
    public static ControllerGrabObject control;
    private SteamVR_TrackedObject trackedObj;
    // 1 Stores the GameObject that the trigger is currently colliding with,
    // so you have the ability to grab the object.
    private GameObject collidingObject;
    // 2 Serves as a reference to the GameObject that the player is currently grabbing.
    private GameObject objectInHand; 
    public GameObject asia, africa, us, europe;
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
    void MapToggle(GameObject curr, GameObject one, GameObject two, GameObject three)
    {
        if (curr.activeSelf)
        {
            curr.SetActive(false);
        }
        else
        {
            curr.SetActive(true);
        }
        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                if (collidingObject.name == "Viewbutton")
                {
                    if (info)
                    {
                        info = false;
                        laserRays.SetActive(true);
                        countryinfo.SetActive(false);
                    }
                    else
                    {
                        info = true;
                        laserRays.SetActive(false);
                        countryinfo.SetActive(true);
                    }
                }
                else if (collidingObject.name == "LocationRay")
                {
                  
                    if (!collidingObject.GetComponent<GlobeLaser>().GetRays().activeSelf)
                    {
                        collidingObject.GetComponent<GlobeLaser>().GetRays().SetActive(true);
                        collidingObject.GetComponent<GlobeLaser>().ChangeColor();
                    }
                    else
                    {
                        collidingObject.GetComponent<GlobeLaser>().GetRays().SetActive(false);
                        collidingObject.GetComponent<GlobeLaser>().ChangeColor();
                    }
                }
                else if (collidingObject.name == "Africa") { MapToggle(africa, us, asia, europe); }
                else if (collidingObject.name == "NorthAmerica") { MapToggle(us, africa, asia, europe); }
                else if (collidingObject.name == "Europe") { MapToggle(europe, us, africa, asia); }
                else if (collidingObject.name == "Asia") { MapToggle(asia, europe, us, africa); }
            }
        }
        if (collidingObject)
        {          
            if (collidingObject.name == "Viewbutton")
            {
                viewhighlight.SetActive(true);
            }          
        }
        else
        {            
            viewhighlight.SetActive(false);
        }

    }
}
