using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{

    private List<Card> deck;
    private List<Card> draw_pile;
    private List<Card> hand;

    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
        
        hand = new List<Card>();
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());

        draw_pile = new List<Card>(deck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Draw(int nb)
    {
        Card tmp;
        for (int i = 0; i < nb; i++)
        {
            if (draw_pile.Count > 1)
            {
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                hand.Add(tmp);
                Debug.Log("Card drawn : " + tmp.card_name);
            }
            else
            {
                Debug.Log("Empty draw pile, cannot draw card");
            }
            
        }
    }
}
