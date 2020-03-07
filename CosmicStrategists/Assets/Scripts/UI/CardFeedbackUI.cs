using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFeedbackUI : MonoBehaviour
{
    public Text CardPlayer1;
    public Text CardPlayer2;

    public float timeDisplayed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WritePlayer1(string text)
    {
        CardPlayer1.text = text;
    }

    public void WritePlayer2(string text)
    {
        CardPlayer2.text = text;
    }

    void Eraseplayer1()
    {
        CardPlayer1.text = "";
    }

    void Eraseplayer2()
    {
        CardPlayer2.text = "";
    }
}
