using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRays : MonoBehaviour {

    private void Start()
    {
        DrawLines();
    }
    void DrawLines()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            Vector3 distance = t.position - transform.position;
            GameObject newVector = new GameObject("newVector");
            newVector.transform.position = transform.position;
            newVector.transform.localScale = distance;
            

        }
    }
}
