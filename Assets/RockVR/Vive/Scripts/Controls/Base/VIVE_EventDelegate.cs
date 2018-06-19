namespace RockVR.Vive
{
    /// <summary>
    /// The Vive button event delegate class
    /// </summary>
    public class VIVE_EventDelegate
    {
        public delegate void PressTrigger();

        public delegate void PressTriggerDown();

        public delegate void PressTriggerUp();

        public delegate void PressGrip();

        public delegate void PressGripDown();

        public delegate void PressGripUp();

        public delegate void PressApplicationMenu();

        public delegate void PressApplicationMenuDown();

        public delegate void PressApplicationMenuUp();

        public delegate void PressTouchpad();

        public delegate void PressTouchpadDown();

        public delegate void PressTouchpadUp();

        public delegate void TouchPadTouch();

        public delegate void TouchPadTouchDown();

        public delegate void TouchPadTouchUp();

        public delegate void SwipeLeft();

        public delegate void SwipeRight();

        public delegate void SwipeTop();

        public delegate void SwipeBottom();

        /// <summary>
        ///  The triggering event when the trigger button is pressed
        /// </summary>
        public PressTrigger OnPressTrigger;
        /// <summary>
        /// The triggering event when the trigger button is pressed down
        /// </summary>
        public PressTriggerDown OnPressTriggerDown;
        /// <summary>
        /// The triggering event when the trigger button is pressed up
        /// </summary>
        public PressTriggerUp OnPressTriggerUp;
        /// <summary>
        /// The triggering event when the Grip button is pressed
        /// </summary>
        public PressGrip OnPressGrip;
        /// <summary>
        /// The triggering event when the Grip button is pressed down
        /// </summary>
        public PressGripDown OnPressGripDown;
        /// <summary>
        /// The triggering event when the Grip button is pressed up
        /// </summary>
        public PressGripUp OnPressGripUp;
        /// <summary>
        /// The triggering event when the applicationMenu button is pressed
        /// </summary>
        public PressApplicationMenu OnPressApplicationMenu;
        /// <summary>
        /// The triggering event when the applicationMenu button is pressed down
        /// </summary>
        public PressApplicationMenuDown OnPressApplicationMenuDown;
        /// <summary>
        /// The triggering event when the applicationMenu button is pressed up
        /// </summary>
        public PressApplicationMenuUp OnPressApplicationMenuUp;
        /// <summary>
        /// The triggering event when the touchpad button is pressed
        /// </summary>
        public PressTouchpad OnPressTouchpad;
        /// <summary>
        /// The triggering event when the touchpad button is pressed down
        /// </summary>
        public PressTouchpadDown OnPressTouchpadDown;
        /// <summary>
        /// The triggering event when the touchpad button is pressed up
        /// </summary>
        public PressTouchpadUp OnPressTouchpadUp;
        /// <summary>
        /// The triggering event when the touchpad button is touched
        /// </summary>
        public TouchPadTouch OnTouchPadTouch;
        /// <summary>
        /// The triggering event when the touchpad button is touched up
        /// </summary>
        public TouchPadTouchUp OnTouchPadTouchUp;
        /// <summary>
        /// The triggering event when the touchpad button is touched down
        /// </summary>
        public TouchPadTouchDown OnTouchPadTouchDown;
        /// <summary>
        /// The triggering event when the touchpad button is Swiped left
        /// </summary>
        public SwipeLeft OnSwipeLeft;
        /// <summary>
        /// The triggering event when the touchpad button is Swiped right
        /// </summary>
        public SwipeRight OnSwipeRight;
        /// <summary>
        /// The triggering event when the touchpad button is Swiped up
        /// </summary>
        public SwipeTop OnSwipeTop;
        /// <summary>
        /// The triggering event when the touchpad button is Swiped down
        /// </summary>
        public SwipeBottom OnSwipeBottom;
    }
}