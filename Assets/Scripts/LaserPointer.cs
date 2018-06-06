using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//attached to right hand! allows teleportation and skipping of steps
public class LaserPointer : MonoBehaviour {
  public GameObject eventsystem;
  Toggle toggle;
   private SteamVR_TrackedObject trackedObj;
   // 1
   public GameObject laserPrefab;
   // 2
   private GameObject laser;
   // 3
   private Transform laserTransform;
   // 4
   /*******FOR RETICLE********/
   public Transform cameraRigTransform; 
   // 2
   public GameObject teleportReticlePrefab;
   // 3
   private GameObject reticle;
   // 4
   private Transform teleportReticleTransform; 
   // 5
   public Transform headTransform; 
   // 6
   public Vector3 teleportReticleOffset; 
   // 7
   public LayerMask teleportMask; 
   // 8
   private bool shouldTeleport; 
   // public GameObject fpscam;
   // FPSCamera camscript;

   private Vector3 hitPoint; 
   private SteamVR_Controller.Device Controller
   {
       get { return SteamVR_Controller.Input((int)trackedObj.index); }
   }

   void Awake()
   {
       trackedObj = GetComponent<SteamVR_TrackedObject>();
   }
   private void ShowLaser(RaycastHit hit)
   {
       // 1
       laser.SetActive(true);
       // 2
       laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
       // 3
       laserTransform.LookAt(hitPoint);
       // 4
       laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
           hit.distance);
   }
   void Start()
   {
   		//camscript = fpscam.GetComponent<FPSCamera>();
       laser = Instantiate(laserPrefab);
       // 2
       laserTransform = laser.transform;
       reticle = Instantiate(teleportReticlePrefab);
       // 2
       teleportReticleTransform = reticle.transform;
       toggle = eventsystem.GetComponent<Toggle>();
   }
   // Update is called once per frame
   void Update () {
       if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
       {
           RaycastHit hit;

           // 2
           if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
           {
               hitPoint = hit.point;
               ShowLaser(hit);
               reticle.SetActive(true);
               // 2
               teleportReticleTransform.position = hitPoint + teleportReticleOffset;
               // 3
               shouldTeleport = true;
           }
       }
       else // 3
       {
           laser.SetActive(false);
           reticle.SetActive(false);
       }
       if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
       {
           Teleport();
       }
       // if (Controller.GetHairTriggerDown()) {
       // 		camscript.SetGrab();
       // 		camscript.CallRend();
       // }
       if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
          if (!toggle.GetCart()) { toggle.SetCart(); }
          else if (!toggle.GetBins()) { toggle.SetBins(); }
          else if (!toggle.GetBin2()) { toggle.SetBin2(); }
          else if (!toggle.GetItem()) { toggle.SetItem(); }
          else if (!toggle.GetPut()) { toggle.SetPut(); }
          else if (!toggle.GetEnd()) { toggle.SetEnd(); }
          else { SceneManager.LoadScene("Scenario2"); }
       }

	}
   private void Teleport()
   {
       // 1
       shouldTeleport = false;
       // 2
       reticle.SetActive(false);
       // 3
       Vector3 difference = cameraRigTransform.position - headTransform.position;
       // 4
       difference.y = 0;
       // 5
       cameraRigTransform.position = hitPoint + difference;
   }
}
