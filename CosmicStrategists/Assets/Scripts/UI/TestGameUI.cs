using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGameUI : MonoBehaviour
{
    public Button drawOneCard;
    public Button discardAllCards;


    // Start is called before the first frame update
    void Start()
    {
        drawOneCard.onClick.AddListener(delegate () { DrawOne(); });
        discardAllCards.onClick.AddListener(delegate () { DiscardAll(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawOne()
    {
        Debug.Log("Draw one card");
    }

    void DiscardAll()
    {
        Debug.Log("Discard all cards");
    }
}
