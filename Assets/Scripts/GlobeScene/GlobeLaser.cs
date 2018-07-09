using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeLaser : MonoBehaviour {
    public GameObject globerays;
    public Material material1;
    public Material material2;
    bool blue = false;
	public GameObject GetRays()
    {
        return globerays;
    }
    public void ChangeColor()
    {
        if (!blue)
        {
            GetComponent<MeshRenderer>().material = material2;
            blue = true;
        } else
        {
            GetComponent<MeshRenderer>().material = material1;
            blue = false;
        }
    }
}
