using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFeedbackUI : MonoBehaviour
{
    public Text CardPlayerA;
    public Text CardPlayerB;

    private bool c_displayed_A = false;
    private bool c_displayed_B = false;

    private float c_timer_A = 0;
    private float c_timer_B = 0;

    public float timeDisplayed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //erase text if timer elapsed
        if (c_displayed_A)
        {
            c_timer_A += Time.deltaTime;
            if (c_timer_A > timeDisplayed)
            {
                EraseplayerA();
                c_timer_A = 0.0f;
            }
        }

        if (c_displayed_B)
        {
            c_timer_B += Time.deltaTime;
            if (c_timer_B > timeDisplayed)
            {
                EraseplayerB();
                c_timer_B = 0.0f;
            }
        }
    }

    public void WritePlayerA(string text)
    {
        CardPlayerA.text = text;
        c_displayed_A = true;
        c_timer_A = 0.0f;
    }

    public void WritePlayerB(string text)
    {
        CardPlayerB.text = text;
        c_displayed_B = true;
        c_timer_B = 0.0f;
    }

    void EraseplayerA()
    {
        CardPlayerA.text = "";
        c_displayed_A = false;
    }

    void EraseplayerB()
    {
        CardPlayerB.text = "";
        c_displayed_B = false;
    }
}
