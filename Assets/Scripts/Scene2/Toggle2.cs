using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle2 : MonoBehaviour
{
    public GameObject ipadhighlight, chargerhighlight, vivehighlight, switchhighlight, penhighlight;
    public GameObject ipad, charger, vive, ninswitch, pen;
    public Text itemName;
    public CanvasGroup WelcomeBox;
    public CanvasGroup Done;
    public CanvasGroup ipadp, chargerp, vivep, switchp, penp;
    public Text error;
    private void Update()
    {
        if (ipadhighlight.activeSelf)
        {
            if (IpadScript.finish && IpadScript2.finish)
            {
                error.text = "";
                ipadhighlight.SetActive(false);
                ipad.SetActive(false);
                chargerhighlight.SetActive(true);
                charger.SetActive(true);
                ipadp.alpha = 0f;
                chargerp.alpha = 1f;
                itemName.text = "Apple Charger";
            }
            if (ControllerGrabObject.control.GetHeld().name == "charger")
            {
                error.text = "Finish placing Ipad";
            }
        } else if (chargerhighlight.activeSelf)
        {
            if (ChargerScript.finish && ChargerScript2.finish)
            {
                error.text = "";
                chargerhighlight.SetActive(false);
                charger.SetActive(false);
                vivehighlight.SetActive(true);
                vive.SetActive(true);
                chargerp.alpha = 0f;
                vivep.alpha = 1f;
                itemName.text = "HTC Vive";
            }
            if (ControllerGrabObject.control.GetHeld().name == "vive")
            {
                error.text = "Finish placing Apple Charger";
            }
        } else if (vivehighlight.activeSelf)
        {
            if (ViveScript.finish && ViveScript2.finish)
            {
                error.text = "";
                vivehighlight.SetActive(false);
                vive.SetActive(false);
                switchhighlight.SetActive(true);
                ninswitch.SetActive(true);
                vivep.alpha = 0f;
                switchp.alpha = 1f;
                itemName.text = "Nintendo Switch";
            }
            if (ControllerGrabObject.control.GetHeld().name == "switch")
            {
                error.text = "Finish placing HTC Vive";
            }
        } else if (switchhighlight.activeSelf)
        {
            if (SwitchScript.finish && SwitchScript2.finish)
            {
                error.text = "";
                switchhighlight.SetActive(false);
                ninswitch.SetActive(false);
                penhighlight.SetActive(true);
                pen.SetActive(true);
                switchp.alpha = 0f;
                penp.alpha = 1f;
                itemName.text = "Apple Pen";
            }
            if (ControllerGrabObject.control.GetHeld().name == "applepen")
            {
                error.text = "Finish placing Apple Pen";
            }
        } else if (penhighlight.activeSelf)
        {
            if (PenScript.finish && PenScript2.finish)
            {
                error.text = "";
                penhighlight.SetActive(false);
                pen.SetActive(false);
                WelcomeBox.alpha = 0f;
                Done.alpha = 1f;
            }
            
        }
    }
}
