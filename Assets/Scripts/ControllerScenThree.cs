using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//attached to the left hand! allows scanning of barcodes and toggling of the log on and off
public class ControllerScenThree : MonoBehaviour
{
    public GameObject map;
    public GameObject button;
    private SteamVR_TrackedObject trackedObj;
    bool on;
    
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        on = false;
     
       
    }

  
    // Update is called once per frame
    void Update()
    {
      
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !on)
        {
            map.SetActive(true);
            button.SetActive(true);
            on = true;
        } else if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && on)
        {
            //close.Play("close");
            map.SetActive(false);
            button.SetActive(false);
            on = false;
        }
        
       
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("Could be used later");
        }
    }

}
