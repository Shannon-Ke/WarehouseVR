using UnityEngine;
using UnityEngine.UI;

public class ControlAnimation : MonoBehaviour
{
    private Animator anim;
    public Slider slider;   //Assign the UI slider of your scene in this slot 

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("TEST_DELETE", -1, slider.normalizedValue);
    }
}