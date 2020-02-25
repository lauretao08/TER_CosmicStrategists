﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGameUI : MonoBehaviour
{
    public Button drawOneCard;
    public Button discardAllCards;

    public GameObject EventManager;
    private CardPlayer card_controller;


    // Start is called before the first frame update
    void Start()
    {
        drawOneCard.onClick.AddListener(delegate () { DrawOne(); });
        discardAllCards.onClick.AddListener(delegate () { DiscardAll(); });
        card_controller = EventManager.GetComponent(typeof(CardPlayer)) as CardPlayer;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawOne()
    {
        card_controller.Draw(1);
        Debug.Log("Draw one card");
    }

    void DiscardAll()
    {
        Debug.Log("Discard all cards");
    }
}
