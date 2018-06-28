using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class vp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<VideoPlayer>().Play();
	}
	
}
