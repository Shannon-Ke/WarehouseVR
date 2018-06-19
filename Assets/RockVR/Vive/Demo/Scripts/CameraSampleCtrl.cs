using UnityEngine;
using System;
using RockVR.Video;

namespace RockVR.Vive.Demo
{
    enum CameraState
    {
        Normal,
        Touched,
        Picked
    }
    public class CameraSampleCtrl : MonoBehaviour
    {
        public ControllerState controllerState = ControllerState.Normal;
        public CameraSetUpCtrl cameraSetUpCtrl;
        public VIVE_FollowCamera[] followCameras;
        public GameObject triggerButton;
        public GameObject gripButton;
        public GameObject touchpadButton;
        public GameObject applicationMenuButton;
        private GameObject cameraObject;
        private CameraState cameraState = CameraState.Normal;
        private bool enableRadialMenu = false;
        private VIVE_Interaction vrIteraction;
        private VIVE_EventCtrl eventCtrl;
        private VIVE_TooltipManager tooltipController;
        private VIVE_Teleport teleport;
        protected VIVE_RadialMenu radiaMenu;

        void Awake()
        {
            vrIteraction = this.transform.GetComponent<VIVE_Interaction>();
            eventCtrl = this.GetComponent<VIVE_EventCtrl>();
            radiaMenu = this.transform.GetComponentInChildren<VIVE_RadialMenu>();
            tooltipController = this.GetComponentInChildren<VIVE_TooltipManager>();
            teleport = this.GetComponent<VIVE_Teleport>();
        }

        private void Start()
        {
            if (controllerState == ControllerState.Ray)
            {
                tooltipController.triggerText = "Pick Camera";
                tooltipController.touchpadText = "Swicth Camera";
                tooltipController.appMenuText = "Start/Stop Capture";
            }
            else if (controllerState == ControllerState.Touch)
            {
                tooltipController.triggerText = "Grab Camera";
                tooltipController.touchpadText = "Teleport";
                tooltipController.appMenuText = "Start/Stop Capture";
            }
            gripButton.SetActive(false);
        }

        void OnEnable()
        {
            if (eventCtrl != null)
            {
                eventCtrl.eventDelegate.OnPressApplicationMenuDown += OnPressApplicationMenuDown;
                eventCtrl.eventDelegate.OnPressTrigger += OnPressTrigger;
                eventCtrl.eventDelegate.OnSwipeLeft += OnSwipeLeft;
                eventCtrl.eventDelegate.OnSwipeRight += OnSwipeRight;
                eventCtrl.eventDelegate.OnPressTriggerUp += OnPressTriggerUp;
                eventCtrl.eventDelegate.OnTouchPadTouch += OnTouchPadTouch;
                eventCtrl.eventDelegate.OnTouchPadTouchUp += OnTouchPadTouchUp;
                eventCtrl.eventDelegate.OnPressTouchpad += OnPressTouchpad;
                eventCtrl.eventDelegate.OnPressTouchpadDown += OnPressTouchpadDown;
                eventCtrl.eventDelegate.OnPressTouchpadUp += OnPressTouchpadUp;
            }
        }

        void OnDisable()
        {
            if (eventCtrl != null)
            {
                eventCtrl.eventDelegate.OnPressApplicationMenuDown -= OnPressApplicationMenuDown;
                eventCtrl.eventDelegate.OnPressTrigger -= OnPressTrigger;
                eventCtrl.eventDelegate.OnSwipeLeft -= OnSwipeLeft;
                eventCtrl.eventDelegate.OnSwipeRight -= OnSwipeRight;
                eventCtrl.eventDelegate.OnPressTriggerUp -= OnPressTriggerUp;
                eventCtrl.eventDelegate.OnTouchPadTouch -= OnTouchPadTouch;
                eventCtrl.eventDelegate.OnPressTouchpad -= OnPressTouchpad;
                eventCtrl.eventDelegate.OnTouchPadTouchUp -= OnTouchPadTouchUp;
                eventCtrl.eventDelegate.OnPressTouchpadDown -= OnPressTouchpadDown;
                eventCtrl.eventDelegate.OnPressTouchpadUp -= OnPressTouchpadUp;
            }
        }

        private void OnPressTouchpad()
        {
            if (controllerState == ControllerState.Touch)
            {
                if (teleport != null)
                {
                    teleport.SearchDropPoint();
                }
            }
        }

        private void OnPressTouchpadDown()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (!radiaMenu) return;
                if (eventCtrl.axisAngle != 0)
                {
                    radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.click);
                }
            }
        }

        private void OnTouchPadTouchUp()
        {
            if (controllerState == ControllerState.Ray)
            {
                radiaMenu.StopTouching();
                radiaMenu.DisableMenu(false);
            }
            else if (controllerState == ControllerState.Touch)
            {
                if (teleport != null)
                {
                    teleport.ConfirmDownPoint();
                    touchpadButton.SetActive(false);
                }
            }
        }

        private void OnTouchPadTouch()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (cameraState == CameraState.Picked && enableRadialMenu)
                {
                    radiaMenu.EnableMenu();
                    if (eventCtrl.axisAngle != 0)
                    {
                        radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.hoverOn);
                    }
                    touchpadButton.SetActive(false);
                }
            }
        }

        private void OnPressTouchpadUp()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (eventCtrl.axisAngle != 0)
                {
                    radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.unclick);
                }
                enableRadialMenu = false;
            }
        }

        private void OnPressApplicationMenuDown()
        {
            if (cameraState == CameraState.Picked || controllerState == ControllerState.Touch)
            {
                if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START ||
                    VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
                {
                    VideoCaptureCtrl.instance.StartCapture();
                    applicationMenuButton.SetActive(false);
                }
                else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }
                else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED)
                {
                    return;
                }
            }
        }

        private void OnPressTrigger()
        {
            if (vrIteraction.selectedObject != null)
            {
                if (vrIteraction.selectedObject.GetComponent<CameraSetUpCtrl>() != null)
                {
                    cameraSetUpCtrl.EnableCamera();
                    if (controllerState == ControllerState.Ray)
                    {
                        foreach (var followCamera in followCameras)
                        {
                            followCamera.followCamera = cameraSetUpCtrl.GetComponent<Camera>();
                        }
                        followCameras[1].OnCameraPointChange();
                        cameraSetUpCtrl.SetCameraScreen();
                        cameraState = CameraState.Picked;
                        enableRadialMenu = true;
                    }
                    else if (controllerState == ControllerState.Touch)
                    {
                        cameraObject = vrIteraction.selectedObject;
                        cameraObject.transform.parent = this.transform;
                        cameraState = CameraState.Touched;
                    }
                    triggerButton.SetActive(false);
                }
            }
        }

        private void OnSwipeRight()
        {
            if (controllerState == ControllerState.Touch)
            {
                if (cameraState == CameraState.Touched)
                {
                    cameraObject.transform.Rotate(Vector3.down, 10);
                    eventCtrl.HapticPulse(3000);
                }
            }
        }

        private void OnSwipeLeft()
        {
            if (controllerState == ControllerState.Touch)
            {
                if (cameraState == CameraState.Touched)
                {
                    cameraObject.transform.Rotate(Vector3.up, 10);
                    eventCtrl.HapticPulse(3000);
                }
            }
        }

        private void OnPressTriggerUp()
        {
            if (controllerState == ControllerState.Touch)
            {
                if (cameraObject != null)
                {
                    cameraObject.transform.parent = null;
                }
            }
        }
    }
}