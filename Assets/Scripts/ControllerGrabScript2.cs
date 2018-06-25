using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabScript2 : MonoBehaviour
{
    public GameObject workers;
    public static ControllerGrabObject control;
    public GameObject dots, front, back;
    private SteamVR_TrackedObject trackedObj;
    // 1 Stores the GameObject that the trigger is currently colliding with,
    // so you have the ability to grab the object.
    private GameObject collidingObject;
    // 2 Serves as a reference to the GameObject that the player is currently grabbing.
    private GameObject objectInHand;
    bool scatter;
    bool bar;
    bool heat;
    public GameObject bargraph;
    public GameObject scatterplot;
    public GameObject heatmap;
    public GameObject heatmaptoggle;
    public GameObject riseup;
    public static GameObject collide;
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
        scatter = false;
        bar = true;
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
        collide = collidingObject;

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
    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {

            if (collidingObject)
            {
                if (scatter && collidingObject.name == "Viewbutton")
                {
                    heatmaptoggle.SetActive(true);
                    scatterplot.SetActive(false);
                    heat = true;
                    scatter = false;
                }
                else if (bar && collidingObject.name == "Viewbutton")
                {
                    scatterplot.SetActive(true);
                    bargraph.SetActive(false);
                    bar = false;
                    scatter = true;
                } else if (heat && collidingObject.name == "Viewbutton")
                {
                    bargraph.SetActive(true);
                    heatmaptoggle.SetActive(false);
                    heat = false;
                    bar = true;
                }
                else if (collidingObject.name == "hideblue" || collidingObject.name == "hidered")
                {
                    if (collidingObject.GetComponent<Hide>().color.activeSelf)
                    {
                        collidingObject.GetComponent<Hide>().color.SetActive(false);
                        collidingObject.GetComponent<Hide>().info.SetActive(false);
                    }
                    else
                    {
                        collidingObject.GetComponent<Hide>().color.SetActive(true);
                        collidingObject.GetComponent<Hide>().info.SetActive(true);
                    }



                } else if (collidingObject.name == "RedBar" || collidingObject.name == "BlueBar"
                    || collidingObject.name == "bluedot" || collidingObject.name == "reddot")
                {
                    GameObject detail = collidingObject.GetComponent<Bars>().Info();
                    if (detail.activeSelf)
                    {
                        detail.SetActive(false);
                    } else if (!detail.activeSelf)
                    {
                        detail.SetActive(true);
                    }
                } else if (collidingObject.name == "Worker")
                {
                    collidingObject.GetComponent<Worker>().ToggleWorker();
                }   else if (collidingObject.name == "WorkerButton")
                {
                    if (workers.activeSelf)
                    {
                        workers.SetActive(false);
                    } else
                    {
                        workers.SetActive(true);
                    }
                    
                }
                else if (collidingObject.name == "Raise")
                {
                    Debug.Log("touched");
                    if (RaiseAnim.risen)
                    {
                        foreach (RaiseAnim r in riseup.GetComponentsInChildren<RaiseAnim>())
                        {
                            r.Fall();
                        }
                        RaiseAnim.risen = false;
                    }
                    else
                    {
                        Debug.Log("shold be here");
                        foreach (RaiseAnim r in riseup.GetComponentsInChildren<RaiseAnim>())
                        {
                            r.Rise();
                        }
                        RaiseAnim.risen = true;
                    }
                }


            }

            

        }
        if (collidingObject)
        {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                //could be map.position - transform.initial
                if (collidingObject.name == "map")
                {
                    collidingObject.transform.position = new Vector3(collidingObject.transform.position.x, transform.position.y, collidingObject.transform.position.z);
                }
                else if (collidingObject.name == "Slide")
                {
                   
                    if (transform.position.x < front.transform.position.x && transform.position.x > back.transform.position.x)
                    {
                       
                        collidingObject.transform.position = new Vector3(transform.position.x, collidingObject.transform.position.y, collidingObject.transform.position.z);
                        foreach (ControlAnimation c in dots.GetComponentsInChildren<ControlAnimation>())
                        {
                            c.Ani((collidingObject.transform.position.x - front.transform.position.x) / (back.transform.position.x - front.transform.position.x));
                        }
                    }
                    
                }
                else if (collidingObject.name == "Slide1")
                {
                    if (transform.position.x < front.transform.position.x && transform.position.x > back.transform.position.x)
                    {

                        collidingObject.transform.position = new Vector3(transform.position.x, collidingObject.transform.position.y, collidingObject.transform.position.z);
                        foreach (ControlAnimation c in heatmap.GetComponentsInChildren<ControlAnimation>())
                        {
                            c.Ani((collidingObject.transform.position.x - front.transform.position.x) / (back.transform.position.x - front.transform.position.x));
                        }
                    }
                }
                
            }
        }

    }
}
