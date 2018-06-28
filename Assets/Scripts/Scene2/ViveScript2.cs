using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveScript2 : MonoBehaviour
{
    public static bool finish;
    private void Start()
    {
        finish = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.name == "viveh (1)")
        {
            finish = true;
        }
    }
}
