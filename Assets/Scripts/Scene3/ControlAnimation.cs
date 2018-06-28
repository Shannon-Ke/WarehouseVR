using UnityEngine;
using UnityEngine.UI;

public class ControlAnimation : MonoBehaviour
{
    private Animation anim;
    public string myanim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play(myanim);
        anim[myanim].speed = 0;
    }

    public void Ani(float value)
    {
       
        anim[myanim].normalizedTime = value;
    }
}