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
    private List<GameObject> hand_game;

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
        hand_game = new List<GameObject>();

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

    void Arrange()
    {
        //ATTENTION : PAS DE VALEURS EN DUR
        Vector3 base_pos = camera_player.transform.position;
        base_pos.z += 2.0f;
        base_pos.x -= (cam_half_width - 2.0f);
        base_pos.y -= 3.0f;

        foreach(GameObject c in hand_game)
        {
            c.transform.position = base_pos;
            base_pos.x +=  3.0f;
        }
    }

    public void Draw(int nb)
    {
        Card tmp;
        GameObject tmp_go;
        for (int i = 0; i < nb; i++)
        {
            if (draw_pile.Count >= 1)
            {
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                hand.Add(tmp);
                Debug.Log("Card drawn : " + tmp.card_name);
                Vector3 card_pos = camera_player.transform.position;
                tmp_go = Instantiate(card_prefab, card_pos, camera_player.transform.rotation);
                hand_game.Add(tmp_go);
                //arrange card position upon drawing
                Arrange();
            }
            else
            {
                Debug.Log("Empty draw pile, cannot draw card");
            }
            
        }
    }
}
