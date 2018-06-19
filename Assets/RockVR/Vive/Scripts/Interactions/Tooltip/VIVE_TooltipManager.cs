using UnityEngine;

namespace RockVR.Vive
{
    /// <summary>
    /// Manage all tooltips.
    /// </summary>
    public class VIVE_TooltipManager : MonoBehaviour
    {
        [Tooltip("The text to display for the trigger button action.")]
        public string triggerText;
        [Tooltip("The text to display for the grip button action.")]
        public string gripText;
        [Tooltip("The text to display for the touchpad action.")]
        public string touchpadText;
        [Tooltip("The text to display for the application menu button action.")]
        public string appMenuText;
        [Tooltip("The colour to use for the tooltip background container.")]
        public Color tipBackgroundColor = Color.black;
        [Tooltip("The colour to use for the text within the tooltip.")]
        public Color tipTextColor = Color.white;
        [Tooltip("The colour to use for the line between the tooltip and the relevant controller button.")]
        public Color tipLineColor = Color.black;
        /// <summary>
        /// Judge the trigger button init.
        /// </summary>
        private bool triggerInit = false;
        /// <summary>
        /// Judge the grip button init.
        /// </summary>
        private bool gripInit = false;
        /// <summary>
        /// Judge the touchpad button init.
        /// </summary>
        private bool touchpadInit = false;
        /// <summary>
        /// Judge the applicationMenu button init.
        /// </summary>
        private bool appMenuInit = false;

        private void Start()
        {
            triggerInit = false;
            gripInit = false;
            touchpadInit = false;
            appMenuInit = false;
        }
        /// <summary>
        /// Init all tooltips
        /// </summary>
        void InitTips()
        {
            foreach (var tooltip in GetComponentsInChildren<VIVE_Tooltip>())
            {
                var tipText = "";
                Transform tipTransform = null;
                switch (tooltip.name.Replace("Tooltip", "").ToLower())
                {
                    case "trigger":
                        tipText = triggerText;
                        tipTransform = GetTransform("trigger");
                        if (tipTransform != null)
                        {
                            triggerInit = true;
                        }
                        break;
                    case "grip":
                        tipText = gripText;
                        tipTransform = GetTransform("lgrip");
                        if (tipTransform != null)
                        {
                            gripInit = true;
                        }
                        break;
                    case "touchpad":
                        tipText = touchpadText;
                        tipTransform = GetTransform("trackpad");
                        if (tipTransform != null)
                        {
                            touchpadInit = true;
                        }
                        break;
                    case "appmenu":
                        tipText = appMenuText;
                        tipTransform = GetTransform("button");
                        if (tipTransform != null)
                        {
                            appMenuInit = true;
                        }
                        break;
                }
                tooltip.containerColor = tipBackgroundColor;
                tooltip.fontColor = tipTextColor;
                tooltip.lineColor = tipLineColor;
                tooltip.Init();
                tooltip.displayText = tipText;
                tooltip.drawLineTo = tipTransform;
                tooltip.Reset();
            }
        }

        private void Update()
        {
            //Whether the initialization is successful. If it is not successful, re-initialize
            if (InitializeSuccess()) InitTips();
        }
        /// <summary>
        /// Whether initialize successfully
        /// </summary>
        private bool InitializeSuccess()
        {
            return !(triggerInit && gripInit && touchpadInit && appMenuInit);
        }
        /// <summary>
        /// Searching corresponding vive trackobject
        /// </summary>
        /// <param name="findTransform"></param>
        /// <returns>The search object</returns>
        private Transform GetTransform(string findTransform)
        {
            return transform.parent.FindChild("Model/" + findTransform + "/attach");
        }
    }
}

