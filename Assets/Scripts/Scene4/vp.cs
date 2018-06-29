using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class vp : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        GetComponent<VideoPlayer>().Play();
    }
  
}
