using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseAnim : MonoBehaviour {
    Animation anim;
    public string rise;
    public string fall;
    public static bool risen;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        
	}
	public void Rise()
    {
        anim.Play(rise);
    }
    public void Fall()
    {
        anim.Play(fall);
    }
}
