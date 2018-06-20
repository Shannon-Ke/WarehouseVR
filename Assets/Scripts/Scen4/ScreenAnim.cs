using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnim : MonoBehaviour {
    public string open;
    public string close;
    public string zoomin;
    public string zoomout;
    public GameObject call;
    private Animation anim;
    bool opened;
     //Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        
    }
	public void InAnim()
    {
        anim.Play(zoomin);
    }
    public void OutAnim()
    {
        anim.Play(zoomout);
    }
    public void CloseAnim()
    {
        anim.Play(close);
    }
    public void OpenAnim()
    {
        anim.Play(open);
    }
    public void Toggle()
    {
        if (opened) { opened = false; }
        else { opened = true; }
    }
    public bool Opened()
    {
        return opened;
    }
    
}
