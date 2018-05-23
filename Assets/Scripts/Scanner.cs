using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

   private SteamVR_TrackedObject trackedObj;
   // 8
   private bool shouldTeleport; 
   public GameObject fpscam;
   FPSCamera camscript;

   private Vector3 hitPoint; 
   private SteamVR_Controller.Device Controller
   {
       get { return SteamVR_Controller.Input((int)trackedObj.index); }
   }

   void Awake()
   {
       trackedObj = GetComponent<SteamVR_TrackedObject>();
   }
   
   void Start()
   {
   		camscript = fpscam.GetComponent<FPSCamera>();
       
   }
   // Update is called once per frame
   void Update () {
       if (Controller.GetHairTriggerDown()) {
       		camscript.SetGrab();
       		camscript.CallRend();
       }
	}
   
}
