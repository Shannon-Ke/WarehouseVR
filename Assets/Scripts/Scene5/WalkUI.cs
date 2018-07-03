using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkUI : MonoBehaviour {

    public Animation aisle1;
    public Animation aisle2;
    public GameObject question;
    bool played = false;
   
    public void OnTriggerEnter(Collider other)
    {
        if (!played && other.isTrigger && other.name == "Eye")
        {
            question.SetActive(true);
            aisle1.Play();
          
            aisle2.Play();
            played = true;
        }
    }
}
