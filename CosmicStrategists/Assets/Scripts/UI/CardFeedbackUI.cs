using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFeedbackUI : MonoBehaviour
{
    public Text CardPlayer1;
    public Text CardPlayer2;

    private bool c_displayed_1 = false;
    private bool c_displayed_2 = false;

    private float c_timer_1 = 0;
    private float c_timer_2 = 0;

    public float timeDisplayed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //erase text if timer elapsed
        if (c_displayed_1)
        {
            c_timer_1 += Time.deltaTime;
            if (c_timer_1 > timeDisplayed)
            {
                Eraseplayer1();
                c_timer_1 = 0.0f;
            }
        }

        if (c_displayed_2)
        {
            c_timer_2 += Time.deltaTime;
            if (c_timer_2 > timeDisplayed)
            {
                Eraseplayer2();
                c_timer_2 = 0.0f;
            }
        }
    }

    public void WritePlayer1(string text)
    {
        CardPlayer1.text = text;
        c_displayed_1 = true;
        c_timer_1 = 0.0f;
    }

    public void WritePlayer2(string text)
    {
        CardPlayer2.text = text;
        c_displayed_2 = true;
        c_timer_2 = 0.0f;
    }

    void Eraseplayer1()
    {
        CardPlayer1.text = "";
        c_displayed_1 = false;
    }

    void Eraseplayer2()
    {
        CardPlayer2.text = "";
        c_displayed_2 = false;
    }
}
