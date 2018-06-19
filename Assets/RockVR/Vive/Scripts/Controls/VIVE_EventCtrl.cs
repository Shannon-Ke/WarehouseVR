using UnityEngine;
//using Valve.VR;
using System;

namespace RockVR.Vive
{
    /// <summary>
    /// The event of Vive's devices status
    /// </summary>
    [RequireComponent(typeof(SteamVR_TrackedObject))]
    public class VIVE_EventCtrl : MonoBehaviour
    {
        /// <summary>
        /// The vive's touchpad device axis
        /// </summary>
        [NonSerialized]
        public Vector2 deviceAxis;
        /// <summary>
        /// The vive's touchpad axis angle
        /// </summary>
        [NonSerialized]
        public float axisAngle;
        public VIVE_EventDelegate eventDelegate = new VIVE_EventDelegate();
        private SteamVR_TrackedObject trackedController;
        private SteamVR_Controller.Device device;
        /// <summary>
        /// The touchpad X axis point
        /// </summary>
        private readonly Vector2 maxXAxis = new Vector2(1, 0);
        /// <summary>
        /// The touchpad Y axis point
        /// </summary>
        private readonly Vector2 maxYAxis = new Vector2(0, 1);
        /// <summary>
        /// Judge is tracking 
        /// </summary>
        private bool trackingSwipe = false;
        /// <summary>
        /// Judge is check 
        /// </summary>
        private bool checkSwipe = false;
        /// <summary>
        /// The min angle rang
        /// </summary>
        private const float minRangeAngle = 30;
        /// <summary>
        /// The min swipe distance
        /// </summary>
        private const float minSwipeDist = 0.2f;
        /// <summary>
        /// The min velocity
        /// </summary>
        private const float minVelocity = 4.0f;
        /// <summary>
        /// The start position
        /// </summary>
        private Vector2 startPosition;
        /// <summary>
        /// The end position
        /// </summary>
        private Vector2 endPosition;
        /// <summary>
        /// The swipe start time
        /// </summary>
        private float swipeStartTime;

        void Awake()
        {
            trackedController = this.GetComponent<SteamVR_TrackedObject>();
        }

        void FixedUpdate()
        {
            device = SteamVR_Controller.Input((int)trackedController.index);
            deviceAxis = device.GetAxis();
            axisAngle = ChangeTouchpadAxisAngle(deviceAxis);
        }

        void Update()
        {
            if (device == null) return;
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                if (eventDelegate.OnPressTrigger != null)
                    eventDelegate.OnPressTrigger();
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                if (eventDelegate.OnPressTriggerDown != null)
                    eventDelegate.OnPressTriggerDown();
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                if (eventDelegate.OnPressTriggerUp != null)
                    eventDelegate.OnPressTriggerUp();
            }
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                if (eventDelegate.OnPressGrip != null)
                {
                    eventDelegate.OnPressGrip();
                }
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                if (eventDelegate.OnPressGripDown != null)
                {
                    eventDelegate.OnPressGripDown();
                }
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                if (eventDelegate.OnPressGripUp != null)
                {
                    eventDelegate.OnPressGripUp();
                }
            }
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                if (eventDelegate.OnPressApplicationMenu != null)
                    eventDelegate.OnPressApplicationMenu();
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                if (eventDelegate.OnPressApplicationMenuDown != null)
                    eventDelegate.OnPressApplicationMenuDown();
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
            {
                if (eventDelegate.OnPressApplicationMenuUp != null)
                    eventDelegate.OnPressApplicationMenuUp();
            }
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnPressTouchpad != null)
                    eventDelegate.OnPressTouchpad();
            }
            if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnPressTouchpadDown != null)
                    eventDelegate.OnPressTouchpadDown();
            }
            if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnPressTouchpadUp != null)
                    eventDelegate.OnPressTouchpadUp();
            }
            if (device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnTouchPadTouch != null)
                    eventDelegate.OnTouchPadTouch();
            }
            if (device.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnTouchPadTouchUp != null)
                    eventDelegate.OnTouchPadTouchUp();
            }
            if (device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (eventDelegate.OnTouchPadTouchDown != null)
                    eventDelegate.OnTouchPadTouchDown();
            }
            if ((int)trackedController.index != -1 && device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                trackingSwipe = true;
                startPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x, device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y);
                swipeStartTime = Time.time;
            }
            else if (device.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                trackingSwipe = false;
                checkSwipe = true;
            }
            else if (trackingSwipe)
            {
                endPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x, device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y);
            }
            if (checkSwipe)
            {
                checkSwipe = false;
                float deltaTime = Time.time - swipeStartTime;
                Vector2 swipeVector = endPosition - startPosition;
                float velocity = swipeVector.magnitude / deltaTime;
                if (velocity > minVelocity && swipeVector.magnitude > minSwipeDist)
                {
                    swipeVector.Normalize();
                    float angleOfSwipe = Vector2.Dot(swipeVector, maxXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                    if (angleOfSwipe < minRangeAngle)
                    {
                        if (eventDelegate.OnSwipeRight != null)
                            eventDelegate.OnSwipeRight();
                    }
                    else if ((180.0f - angleOfSwipe) < minRangeAngle)
                    {
                        if (eventDelegate.OnSwipeLeft != null)
                            eventDelegate.OnSwipeLeft();
                    }
                    else
                    {
                        angleOfSwipe = Vector2.Dot(swipeVector, maxYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < minRangeAngle)
                        {
                            if (eventDelegate.OnSwipeTop != null)
                                eventDelegate.OnSwipeTop();
                        }
                        else if ((180.0f - angleOfSwipe) < minRangeAngle)
                        {
                            if (eventDelegate.OnSwipeBottom != null)
                                eventDelegate.OnSwipeBottom();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Control handle vibration method 
        /// </summary>
        /// <param name="index"> Vibration numerical</param>
        public void HapticPulse(int index)
        {
            device.TriggerHapticPulse(ushort.Parse(index.ToString()));
        }

        /// <summary>
        /// Judgment of sliding angle method
        /// </summary>
        /// <param name="axis"></param>
        /// <returns>The number of angle</returns>
        private float ChangeTouchpadAxisAngle(Vector2 axis)
        {
            float angle = Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg;
            angle = 90.0f - angle;
            if (angle < 0)
            {
                angle += 360.0f;
            }
            return 360 - angle;
        }
    }
}
