using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour {
    public GameObject eventcontroller;
    Items items;
    public GameObject fpscam;
    FPSCamera camscript;
    public CanvasGroup open, welcome, instruction, log;
    //public CanvasGroup scanner;
    public Text instrText, scantext, user, error, message, logtxt;

    public CanvasGroup scan1, scan2, done1, done2;
    bool cart, bins, logtoggle, bin1, bin2, location, item, put, end;
    int itemcount = 0;
    string bin1string = "";
    int numitems;
   string currname = "";
   int currnum = 0;
	// Use this for initialization
	void Start()
	{  
        cart = false;
        bins = false;
        logtoggle = false;
        bin1 = false;
        bin2 = false;
        location = false;
        item = false;
        put = false;
        end = false;
        camscript = fpscam.GetComponent<FPSCamera>();
        items = eventcontroller.GetComponent<Items>();
        numitems = items.Numitems();
	}
	void Awake () {
        welcome.alpha = 0f;
        instruction.alpha = 0f;
        log.alpha = 0f;
        scan1.alpha = 0f;
        scan2.alpha = 0f;
        done1.alpha = 0f;
        done2.alpha = 0f;
        //scanner.alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (camscript.GetText() != null) {
            message.text = "scanned QR is " + camscript.GetText();
            if (string.Equals("ID", camscript.GetText().Substring(0, 2)) && !cart)
            {
                error.text = "";
                
                welcome.alpha = 1f;
                open.alpha = 0f;
                

                user.text = "Welcome, " + camscript.GetText().Substring(5, camscript.GetText().Length - 5);
                logtxt.text = "LOG:\n" + camscript.GetText();
                cart = true;
                scantext.text = "Scan cart to begin";
                camscript.ResetText();
            }
            else if (!cart) {
                error.text = "Must scan valid ID";
            }
            else if (string.Equals("Cart", camscript.GetText().Substring(0, 4)) && !bins)
            {
                logtxt.text += "\nCart : " + camscript.GetText();
                instruction.alpha = 1f;
                instrText.text = "Set up Cart Bins";
                
                error.text = "";
                user.text = "Scan and Place Bins";
                scantext.text = "";
                scan1.alpha = 1f;
                scan2.alpha = 1f;
                bins = true;
                camscript.ResetText(); 
            }
            else if (!bins) {
                error.text = "Must scan valid cart ID";
            } else if (string.Equals("Bin", camscript.GetText().Substring(0, 3)) && !bin2) {
                error.text = "";

                if (!bin1) {
                    scan1.alpha = 0f;
                    done1.alpha =  1f;
                    bin1 = true;
                    logtxt.text += "\nBin One : " + camscript.GetText();
                    bin1string = camscript.GetText();
                } else {
                    if (string.Equals(bin1string, camscript.GetText())) {
                        error.text = "Bin already in use";
                    } else {
                        scan2.alpha = 0f;
                        done2.alpha = 1f;
                        bin2 = true;
                        logtxt.text += "\nBin Two : " + camscript.GetText();
                    }
                }
                camscript.ResetText();
                if (bin2) {
                    int count = 0;
                    while (count < 1000) {
                        count++;
                    }
                    Transition();
                }
                
            }
            else if (!bin2) {
                error.text = "Must scan valid bin";
            }
            
            else if (string.Equals(user.text, camscript.GetText()) && !item && numitems > 0) {
                
                currname = items.GetTask1Keys()[numitems - 1];
                currnum = items.GetTask1Values()[numitems - 1];
                error.text = "";
                item = true;
                instrText.text = currnum + " Units";
                user.text = currname + "\nItem ID: 10003";
                scantext.text = "Scan item 0/" + currnum;
                logtxt.text += "\nLocation : " + camscript.GetText();
                camscript.ResetText();
                
            }
            else if (!item) { error.text = "Must scan correct location."; }
            else if (string.Equals(currname, camscript.GetText()) && !put) {
                error.text = "";
                itemcount++;
                if (itemcount == currnum) {
                    put = true;
                    logtxt.text += "\nItem : " + currname + " (" + currnum + ")";
                    instrText.text = "Put in Bins";
                }
                scantext.text = "Scan item " + itemcount + "/" + currnum;

                camscript.ResetText();
            }
            else if (!put) { error.text = "Must scan correct item."; }
            else if (!end) {
                
                if (string.Equals("ID : Shannon Ke", camscript.GetText())) {
                    Debug.Log("WTF ISN'T THIS WORKING");
                    end = true;
                    camscript.ResetText();
                    if (numitems > 0) { 
                numitems--;
                if (numitems == 0) {
                    instrText.text = "DONE";
                } else {
                    end = false;
                    item = false;
                    put = false;
                    itemcount = 0;
                    instrText.text = "Go to";
                    user.text = items.GetTask1Locations()[numitems - 1];
                    scantext.text = "Scan Location";
                }
                }
            }
            
                
            } 
        }
        
	}
    void Transition() {
        instrText.text = "Go to";
        user.text = items.GetTask1Locations()[numitems - 1];
        scantext.text = "Scan Location";
        done1.alpha = 0f;
        done2.alpha = 0f;
    }
    public void Openlog() {
        log.alpha = 1f;
    }
    public void Closelog() {
        log.alpha = 0f;
    }
    public void SetCart() {
        cart = true;
        error.text = "";
                
        welcome.alpha = 1f;
        open.alpha = 0f;
        

        user.text = "Welcome, USER";
        logtxt.text = "LOG:\nID : skipped";
        
        scantext.text = "Scan cart to begin";
        camscript.ResetText();
    }
    public void SetBins() {
        logtxt.text += "\nCart : skipped";
                instruction.alpha = 1f;
                instrText.text = "Set up Cart Bins";
                
                error.text = "";
                user.text = "Scan and Place Bins";
                scantext.text = "";
                scan1.alpha = 1f;
                scan2.alpha = 1f;
                bins = true;
                camscript.ResetText(); 
    }
    public void SetBin2() {
        bin2 = true;
        scan1.alpha = 0f;
        scan2.alpha = 0f;
        Transition();
        logtxt.text += "\nBins : skipped";
    }
    public void SetItem() {
        error.text = "";
                item = true;
                instrText.text = "3 Units";
                user.text = "Blue Ball\nItem ID: 10003";
                scantext.text = "Scan item 0/3";
                logtxt.text += "\nLocation : skipped";
                camscript.ResetText();
    }
    public void SetPut() {
    
                scantext.text = "Scan item " + itemcount + "/3";
                logtxt.text += "\nItem : skipped";
                camscript.ResetText();
    }
    public bool GetCart() {
        return cart;
    }

    public bool GetBins() {
        return bins;
    }
    public bool GetBin2() {
        return bin2;
    }
    public bool GetItem() {
        return item;
    }
    public bool GetPut() {
        return put;
    }
}
