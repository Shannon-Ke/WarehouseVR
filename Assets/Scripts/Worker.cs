using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {
    public GameObject dot;
    public GameObject bar;
	public void ToggleWorker()
    {
        if (dot.activeSelf)
        {
            dot.SetActive(false);
            bar.SetActive(false);
        } else
        {
            dot.SetActive(true);
            bar.SetActive(true);
        }
    }
}
