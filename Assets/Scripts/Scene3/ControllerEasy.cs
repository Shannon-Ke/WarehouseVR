using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class ControllerEasy : MonoBehaviour
{
    
    private SteamVR_TrackedObject trackedObj;
    // 1 Stores the GameObject that the trigger is currently colliding with,
    // so you have the ability to grab the object.
    private GameObject collidingObject;
    // 2 Serves as a reference to the GameObject that the player is currently grabbing.
    private GameObject objectInHand;
    bool scatter;
    bool bar;
    bool heat;
    bool world;
    public GameObject bargraph;
    public GameObject scatterplot;
    
    public GameObject heatmap;
    public GameObject globe;
   
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
        heat = false;
        world = false;
        scatter = false;
        bar = true;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (scatter)
            {
                globe.SetActive(true);
                scatterplot.SetActive(false);
                world = true;
                scatter = false;
            }
            else if (bar)
            {
                heatmap.SetActive(true);
                bargraph.SetActive(false);
                bar = false;
                heat = true;
            }
            else if (heat)
            {
                scatterplot.SetActive(true);
                heatmap.SetActive(false);
                heat = false;
                scatter = true;
            }
            else if (globe)
            {
                bargraph.SetActive(true);
                globe.SetActive(false);
                world = false;
                bar = true;
            }
        }
      

    }
}
