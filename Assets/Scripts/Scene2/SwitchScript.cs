﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {
    public static bool finish;
    private void Start()
    {
        finish = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.name == "switchh")
        {

            finish = true;
        }
    }
}