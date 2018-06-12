using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFind : MonoBehaviour {
    public GameObject nextArrow;
    public GameObject prevArrow;
	// Use this for initialization
	
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "pCube741")
        {
            prevArrow.SetActive(false);
            nextArrow.SetActive(true);
        }
    }
}
