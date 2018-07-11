using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    public GameObject slider, one, two;
    public CanvasGroup choose, wanttoview, plaidshirt;
    bool item = false;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (wanttoview.alpha == 1f)
            {
                two.SetActive(true);
                one.SetActive(false);
                plaidshirt.alpha = 1f;
                wanttoview.alpha = 0f;
            }
        }
        else if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (!item)
            {
                item = true;
                slider.SetActive(false);
                choose.alpha = 0f;
                wanttoview.alpha = 1f;
            } else
            {
                one.SetActive(true);
                two.SetActive(false);
                item = false;
                slider.SetActive(true);
                choose.alpha = 1f;
                wanttoview.alpha = 0f;
                plaidshirt.alpha = 0f;
            }
        }
    }
}
