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
    private void Update()
    {
        if (ipadhighlight.activeSelf)
        {
            if (IpadScript.finish)
            {
                ipadhighlight.SetActive(false);
                ipad.SetActive(false);
                chargerhighlight.SetActive(true);
                charger.SetActive(true);
                ipadp.alpha = 0f;
                chargerp.alpha = 1f;
                itemName.text = "Apple Charger";
            }
        } else if (chargerhighlight.activeSelf)
        {
            if (ChargerScript.finish)
            {
                chargerhighlight.SetActive(false);
                charger.SetActive(false);
                vivehighlight.SetActive(true);
                vive.SetActive(true);
                chargerp.alpha = 0f;
                vivep.alpha = 1f;
                itemName.text = "HTC Vive";
            }
        } else if (vivehighlight.activeSelf)
        {
            if (ViveScript.finish)
            {
                vivehighlight.SetActive(false);
                vive.SetActive(false);
                switchhighlight.SetActive(true);
                ninswitch.SetActive(true);
                vivep.alpha = 0f;
                switchp.alpha = 1f;
                itemName.text = "Nintendo Switch";
            }
        } else if (switchhighlight.activeSelf)
        {
            if (SwitchScript.finish)
            {
                switchhighlight.SetActive(false);
                ninswitch.SetActive(false);
                penhighlight.SetActive(true);
                pen.SetActive(true);
                switchp.alpha = 0f;
                penp.alpha = 1f;
                itemName.text = "Apple Pen";
            }
        } else if (penhighlight.activeSelf)
        {
            if (PenScript.finish)
            {
                penhighlight.SetActive(false);
                pen.SetActive(false);
                WelcomeBox.alpha = 0f;
                Done.alpha = 1f;
            }
        }
    }
}
