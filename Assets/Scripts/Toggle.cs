using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour {
    public GameObject fpscam;
    FPSCamera camscript;
    public CanvasGroup open;
    public CanvasGroup welcome;
    //public CanvasGroup scanner;
    public Text scantext;
    public Text user;
    public Text error;
    public Text message;
    bool tasks = false;
	// Use this for initialization
	void Start()
	{
        camscript = fpscam.GetComponent<FPSCamera>();
	}
	void Awake () {
        welcome.alpha = 0f;
        //scanner.alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (camscript.GetText() != null) {
            message.text = "scanned QR is " + camscript.GetText();
            if (string.Equals("ID", camscript.GetText().Substring(0, 2)) && !tasks)
            {
                error.text = "";
                //if (string.Equals(camscript.GetText(), "Shannon Ke") && !tasks) {
                welcome.alpha = 1f;
                open.alpha = 0f;
                //scanner.alpha = 1f;
                // if (camscript.GetText() != null) {
                //     Debug.Log("worked and script was: " + camscript.GetText());
                // } else {
                //     Debug.Log("didn't print");
                // }

                user.text = "Welcome, " + camscript.GetText().Substring(5, camscript.GetText().Length - 5);
                tasks = true;
                scantext.text = "Scan task to begin";
                camscript.ResetText();
            }
            else if (!tasks) {
                error.text = "Must scan valid ID";
            }
            else if (string.Equals("Task", camscript.GetText().Substring(0, 4)))
            {
                error.text = "";
                user.text = "Enter Task " + camscript.GetText().Substring(5, camscript.GetText().Length - 5);
                scantext.text = "Scan cart IDs";

            }
            else {
                error.text = "Must scan valid task ID";
            }
            
        } 
	}

}
