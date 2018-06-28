using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IpadScript2 : MonoBehaviour
{

    public static bool finish;
    private void Start()
    {
        finish = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.name == "ipadh (1)")
        {

            finish = true;
        }
    }
}
