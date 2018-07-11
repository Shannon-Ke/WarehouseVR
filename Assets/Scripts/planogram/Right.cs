using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    public Material heatmap;
    public Material defaultFloor;
    public GameObject floor;
    public CanvasGroup choose;
    bool heat = false;
    public GameObject highlights;
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
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (!heat)
            {
                heat = true;
                choose.alpha = 0f;
                floor.GetComponent<MeshRenderer>().material = heatmap;
                highlights.SetActive(true);
            } else
            {
                heat = false;
                choose.alpha = 1f;
                floor.GetComponent<MeshRenderer>().material = defaultFloor;
                highlights.SetActive(false);
            }
            
        }
    }
}
