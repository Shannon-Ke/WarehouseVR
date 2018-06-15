using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayFind : MonoBehaviour {
    public GameObject nextArrow;
    public GameObject prevArrow;
    public Text message;
	// Use this for initialization
	
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "pCube741")
        {
            message.text = "";
            prevArrow.SetActive(false);
            nextArrow.SetActive(true);
        }
        //else
        //{
        //    message.text = "Don't forget your cart!";
        //}
    }
}
