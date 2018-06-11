using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerScript2 : MonoBehaviour
{
    public static bool finish;
    private void Start()
    {
        finish = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.name == "chargerh (1)")
        {

            finish = true;
        }
    }
}
