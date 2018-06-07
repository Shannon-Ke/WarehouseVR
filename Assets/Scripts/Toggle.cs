using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{

    public GameObject bin1o;
    public GameObject bin2o;
    public GameObject controller;
    bool item1 = false;
    bool isplaced = false;

    public GameObject eventcontroller;
    Items items;
    public GameObject fpscam;
    FPSCamera camscript;
    public CanvasGroup open, welcome, instruction, log, bin1highlight, bin2highlight, loc1highlight, loc2highlight;

    //public CanvasGroup scanner;
    public Text instrText, scantext, user, error, message, logtxt, unitstxt, bin1txt, bin2txt;

    public CanvasGroup scan1, scan2, done1, done2;
    public CanvasGroup needfill, needfill2, donebin, donebin2, graphic, notneed, notneed2;
    public GameObject ball1, ball2, ball3, box1, box2;
    bool cart, bins, logtoggle, bin1, bin2, location, item, put, end, donebins;
    int itemcount = 0;
    string bin1string = "";
    int numitems;
    int bincount;
    string currname = "";
    int currnum = 0;
    // Use this for initialization
    void Start()
    {
        ball1.SetActive(false);
        ball2.SetActive(false);
        ball3.SetActive(false);
        box1.SetActive(false);
        box2.SetActive(false);
        Reset();
        camscript = fpscam.GetComponent<FPSCamera>();
        items = eventcontroller.GetComponent<Items>();

        numitems = items.Numitems();
        bincount = 0;
        bin1highlight.alpha = 0f;
        bin2highlight.alpha = 0f;
        loc1highlight.alpha = 0f;
        loc2highlight.alpha = 0f;

    }
    void Awake()
    {
        welcome.alpha = 0f;
        instruction.alpha = 0f;
        log.alpha = 0f;
        scan1.alpha = 0f;
        scan2.alpha = 0f;
        done1.alpha = 0f;
        done2.alpha = 0f;
        ResetBins();
        //scanner.alpha = 0f;
    }
    void ResetBins()
    {
        needfill.alpha = 0f;
        needfill2.alpha = 0f;
        donebin.alpha = 0f;
        donebin2.alpha = 0f;
        graphic.alpha = 0f;
        notneed.alpha = 0f;
        notneed2.alpha = 0f;
    }
    void Reset()
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
        donebins = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (camscript.GetText() != null)
        {
            //message.text = "scanned QR is " + camscript.GetText();
            if (string.Equals("ID", camscript.GetText().Substring(0, 2)) && !cart)
            {
                error.text = "";

                welcome.alpha = 1f;
                open.alpha = 0f;

                instruction.alpha = 1f;
                user.text = "Welcome, " + camscript.GetText().Substring(5, camscript.GetText().Length - 5);
                logtxt.text = "LOG:\n" + camscript.GetText();
                cart = true;
                scantext.text = "Scan cart to begin";
                camscript.ResetText();
            }
            else if (!cart)
            {
                error.text = "Must scan valid ID";
            }
            else if (string.Equals("Cart", camscript.GetText().Substring(0, 4)) && !bins)
            {
                logtxt.text += "\nCart : " + camscript.GetText();

                instrText.text = "Set up Cart Bins";

                error.text = "";
                user.text = "Scan and Place Bins";
                scantext.text = "";
                scan1.alpha = 1f;
                scan2.alpha = 1f;
                bins = true;
                camscript.ResetText();
            }
            else if (!bins)
            {
                error.text = "Must scan valid cart ID";
            }
            else if (string.Equals("Bin", camscript.GetText().Substring(0, 3)) && !bin2)
            {
                error.text = "";

                if (!bin1)
                {
                    scan1.alpha = 0f;
                    done1.alpha = 1f;
                    bin1 = true;
                    logtxt.text += "\nBin One : " + camscript.GetText();
                    bin1string = camscript.GetText();
                    if (string.Equals(bin1string, "Bin002"))
                    {
                        CanvasGroup temp = bin1highlight;
                        bin1highlight = bin2highlight;
                        bin2highlight = temp;
                        GameObject temp1 = bin1o;
                        bin1o = bin2o;
                        bin2o = temp1;
                    }
                }
                else
                {
                    if (string.Equals(bin1string, camscript.GetText()))
                    {
                        error.text = "Bin already in use";
                    }
                    else
                    {
                        scan2.alpha = 0f;
                        done2.alpha = 1f;
                        bin2 = true;
                        logtxt.text += "\nBin Two : " + camscript.GetText();
                    }
                }
                camscript.ResetText();
                if (bin2)
                {
                    done1.alpha = 0f;
                    done2.alpha = 0f;
                    instrText.text = "Go to";
                    user.resizeTextForBestFit = true;
                    user.text = items.GetTask1Locations()[numitems - 1];
                    scantext.text = "Scan Location";
                    if (!item1)
                    {
                        loc2highlight.alpha = 1f;

                    }

                }

            }
            else if (!bin2)
            {
                error.text = "Must scan valid bin";
            }

            else if (string.Equals(user.text, camscript.GetText()) && !item && numitems > 0)
            {
                if (!item1)
                {
                    loc2highlight.alpha = 0f;
                }
                else
                {
                    loc1highlight.alpha = 0f;
                }
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
            else if (string.Equals(currname, camscript.GetText()) && !put)
            {

                error.text = "";
                itemcount++;
                if (itemcount == currnum)
                {
                    put = true;
                    logtxt.text += "\nItem : " + currname + " (" + currnum + ")";
                    instrText.text = "Put in Bins";
                    user.text = "";
                    scantext.text = "";
                    unitstxt.text = currnum + "\nUnits";
                    graphic.alpha = 1f;
                    if (items.GetUnits()[numitems - 1, 0] != 0)
                    {
                        bin1txt.text = items.GetUnits()[numitems - 1, 0] + "";
                        needfill.alpha = 1f;
                    }
                    else
                    {
                        notneed.alpha = 1f;
                    }
                    if (items.GetUnits()[numitems - 1, 1] != 0)
                    {
                        bin2txt.text = items.GetUnits()[numitems - 1, 1] + "";
                        needfill2.alpha = 1f;
                    }
                    else
                    {
                        notneed2.alpha = 1f;
                    }
                }
                else { scantext.text = "Scan item " + itemcount + "/" + currnum; }

                camscript.ResetText();
            }
            else if (!put) { error.text = "Must scan correct item."; }

        }
        else if (put && !end)
        {
            //figure out why you have to scan something before you get here!
            //if (string.Equals("ID : Shannon Ke", camscript.GetText())) {
            //Debug.Log("end reached");
            if (!donebins)
            {

                if (needfill.alpha == 1f)
                {
                    bin1highlight.alpha = 1f;
                    ball1.SetActive(true);
                    ball2.SetActive(true);

                    if (bincount == items.GetUnits()[numitems - 1, 0])
                    {
                        needfill.alpha = 0f;
                        donebin.alpha = 1f;
                        bincount = 0;
                        bin1highlight.alpha = 0f;
                        ball1.SetActive(false);
                        ball2.SetActive(false);
                    }
                }
                else
                {
                    if (needfill2.alpha == 1f)
                    {
                        bin2highlight.alpha = 1f; //use currnum variable
                        ball3.SetActive(true);
                        box1.SetActive(true);
                        box2.SetActive(true);

                        if (bincount == items.GetUnits()[numitems - 1, 1])
                        {
                            ball3.SetActive(false);
                            box1.SetActive(false);
                            box2.SetActive(false);
                            needfill2.alpha = 0f;
                            donebin2.alpha = 1f;
                            bincount = 0;
                            bin2highlight.alpha = 0f;
                            donebins = true;
                        }
                    }
                    else if (notneed2.alpha == 1f)
                    {
                        donebins = true;
                    }
                }
            }
            else
            {
                ResetBins();
                end = true;
                unitstxt.text = "";
                if (numitems > 0)
                {
                    numitems--;
                    if (numitems == 0)
                    {
                        instrText.text = "DONE";
                        user.text = "End of Tote";
                    }
                    else
                    {
                        item1 = true;
                        end = false;
                        item = false;
                        put = false;
                        donebins = false;
                        itemcount = 0;
                        instrText.text = "Go to";
                        user.text = items.GetTask1Locations()[numitems - 1];
                        scantext.text = "Scan Location";
                        loc1highlight.alpha = 1f;
                    }

                }
            }
        }

    }
    void Transition()
    {
        instrText.text = "Go to";
        user.text = items.GetTask1Locations()[numitems - 1];
        scantext.text = "Scan Location";

    }
    public void Openlog()
    {
        log.alpha = 1f;
    }
    public void Closelog()
    {
        log.alpha = 0f;
    }
    public void SetCart()
    {
        cart = true;
        error.text = "";

        welcome.alpha = 1f;
        open.alpha = 0f;
        instruction.alpha = 1f;

        user.text = "Welcome, USER";
        logtxt.text = "LOG:\nID : skipped";

        scantext.text = "Scan cart to begin";
        camscript.ResetText();
    }
    public void SetBins()
    {
        logtxt.text += "\nCart : skipped";

        instrText.text = "Set up Cart Bins";
        error.text = "";
        user.text = "Scan and Place Bins";
        scantext.text = "";
        scan1.alpha = 1f;
        scan2.alpha = 1f;
        bins = true;
        camscript.ResetText();
    }
    public void SetBin2()
    {
        bin2 = true;
        scan1.alpha = 0f;
        scan2.alpha = 0f;
        Transition();
        logtxt.text += "\nBins : skipped";
        done1.alpha = 0f;
        done2.alpha = 0f;
        instrText.text = "Go to";
        user.resizeTextForBestFit = true;
        user.text = items.GetTask1Locations()[numitems - 1];
        scantext.text = "Scan Location";
        if (!item1)
        {
            loc2highlight.alpha = 1f;

        }
    }
    public void SetItem()
    {
        if (!item1)
        {
            loc2highlight.alpha = 0f;
        }
        else
        {
            loc1highlight.alpha = 0f;
        }
        currname = items.GetTask1Keys()[numitems - 1];
        currnum = items.GetTask1Values()[numitems - 1];
        error.text = "";
        item = true;
        instrText.text = currnum + " Units";
        user.text = currname + "\nItem ID: 10003";
        scantext.text = "Scan item 0/" + currnum;
        logtxt.text += "\nLocation : skipped";

        camscript.ResetText();
    }
    public void SetPut()
    {
        put = true;
        logtxt.text += "\nItem : " + currname + " (" + currnum + ")";
        instrText.text = "Put in Bins";
        user.text = "";
        scantext.text = "";
        unitstxt.text = currnum + "\nUnits";
        graphic.alpha = 1f;
        if (items.GetUnits()[numitems - 1, 0] != 0)
        {
            bin1txt.text = items.GetUnits()[numitems - 1, 0] + "";
            needfill.alpha = 1f;
        }
        else
        {
            notneed.alpha = 1f;
        }
        if (items.GetUnits()[numitems - 1, 1] != 0)
        {
            bin2txt.text = items.GetUnits()[numitems - 1, 1] + "";
            needfill2.alpha = 1f;
        }
        else
        {
            notneed2.alpha = 1f;
        }
    }

    public void SetEnd()
    {
        ResetBins();
        end = true;
        unitstxt.text = "";
        if (numitems > 0)
        {
            numitems--;
            if (numitems == 0)
            {
                instrText.text = "DONE";
                user.text = "End of Tote";
            }
            else
            {
                item1 = true;
                end = false;
                item = false;
                put = false;
                donebins = false;
                itemcount = 0;
                instrText.text = "Go to";
                user.text = items.GetTask1Locations()[numitems - 1];
                scantext.text = "Scan Location";
                loc1highlight.alpha = 1f;
            }

        }
    }
    public bool GetCart()
    {
        return cart;
    }

    public bool GetBins()
    {
        return bins;
    }
    public bool GetBin2()
    {
        return bin2;
    }
    public bool GetItem()
    {
        return item;
    }
    public bool GetPut()
    {
        return put;
    }

    public bool GetEnd()
    {
        return end;
    }
    public void IncrementBin()
    {
        bincount++;
    }
}
