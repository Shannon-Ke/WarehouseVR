// Attach this script to a Camera
//Also attach a GameObject that has a Renderer (e.g. a cube) in the Display field
//Press the space key in Play mode to capture

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class FPSCamera : MonoBehaviour
{
    // Grab the camera's view when this variable is true.
    bool grab;
    string final;
    // public Camera mainCamera;
    // public Camera scanCamera;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    // The "m_Display" is the GameObject whose Texture will be set to the captured image.
    public Renderer m_Display;
    public RenderTexture rt;
    void Awake()
   {
       trackedObj = GetComponent<SteamVR_TrackedObject>();
       
   }
   // void Start() {
   //  mainCamera.enabled = true;
   //     scanCamera.enabled = false;
   // }
    // private void Update()
    // {
    //     //Press space to start the screen grab
    //     if (Controller.GetHairTriggerDown())
    //         grab = true;
    // }
    public void CallRend() {
        // scanCamera.enabled = true;
        //     mainCamera.enabled = false;
        OnPostRender();
    }
    private void OnPostRender()
    {
        if (grab)
        {
            //Create a new texture with the width and height of the screen
            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = rt;
            Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0,0, rt.width, rt.height), 0, 0, false);
            texture.Apply();

            //Check that the display field has been assigned in the Inspector
            if (m_Display != null)
                //Give your GameObject with the renderer this texture
                m_Display.material.mainTexture = texture;
            
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(texture.GetPixels32(),
                  texture.width, texture.height);
                //if (result != null)
                //{
                //    Debug.Log("DECODED TEXT FROM QR: " + result.Text + Screen.currentResolution);
                //}
                final = result.Text;
            }
            catch (System.Exception ex) { Debug.LogWarning(ex.Message); }
            //Reset the grab state
            grab = false;
            RenderTexture.active = currentActiveRT;
            // mainCamera.enabled = true;
            // scanCamera.enabled = false;

        }
    }
    public string GetText() {
        return final;
    }
    public bool GetGrab() {
        return grab;
    }
    public void ResetText() {
        final = null;
    }
    public void SetGrab() {
        grab = true;
    }
}