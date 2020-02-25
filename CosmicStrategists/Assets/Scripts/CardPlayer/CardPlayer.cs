using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public Camera camera_player;
    public GameObject card_prefab;


    private List<Card> deck;
    private List<Card> draw_pile;
    private List<Card> hand;

    //*Aspect ratio related parameters*//
    float screen_aspect;
    float cam_half_height;
    float cam_half_width;

    public int max_number_cards_hand;


    // Start is called before the first frame update
    void Start()
    {
        deck = new List<Card>();
        
        hand = new List<Card>();
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());

        draw_pile = new List<Card>(deck);

        /**ASPECT RATIO**/
        screen_aspect = (float)Screen.width / (float)Screen.height;
        cam_half_height = camera_player.orthographicSize;
        cam_half_width = screen_aspect * cam_half_height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(int nb)
    {
        Card tmp;
        for (int i = 0; i < nb; i++)
        {
            if (draw_pile.Count >= 1)
            {
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                hand.Add(tmp);
                Debug.Log("Card drawn : " + tmp.card_name);
                Vector3 card_pos = camera_player.transform.position;
                card_pos.z += 10;
                Instantiate(card_prefab, card_pos, camera_player.transform.rotation);
            }
            else
            {
                Debug.Log("Empty draw pile, cannot draw card");
            }
            
        }
    }
}
