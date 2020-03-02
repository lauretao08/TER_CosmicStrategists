using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public Camera camera_player;
    public GameObject card_prefab;
	public Player player;


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
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
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
        base_pos.x -= (cam_half_width - 1.5f);
        base_pos.y -= 3.3f;
		
        Card tmp_card;
        foreach(GameObject c in hand_game)
        {
            c.transform.position = base_pos;
            //ATTENTION : AMELIORATION NECESSAIRE !
            tmp_card = c.GetComponent(typeof(Card)) as Card;
            if (tmp_card != null)
            {
                tmp_card.SetHandPosition(base_pos);
            }
            else
            {
                Debug.Log("ERROR : NO CARD FOR SETHANDPOSITION");
            }
            base_pos.x +=  2.1f;
        }
    }

    public void Draw(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
			if (hand.Count >= max_number_cards_hand){
                Debug.Log("Hand full ");
				return;
			}
			
            if (draw_pile.Count >= 1)
            {				
				Card tmp = new Card();
				Card tmp_card = new Card();
				GameObject tmp_go;
				
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                //hand.Add(tmp);
                Debug.Log("Card drawn : " + tmp.card_name);
                Vector3 card_pos = camera_player.transform.position;
                tmp_go = Instantiate(card_prefab, card_pos, camera_player.transform.rotation);
                hand_game.Add(tmp_go);

                //OPTIMIZE THIS GETCOMPONENT!
                tmp_card = tmp_go.GetComponent(typeof(Card)) as Card;
                tmp_card.SetCardManager(this);
                hand.Add(tmp_card);
                //arrange card position upon drawing
                Arrange();
            }
            else
            {
                Debug.Log("Empty draw pile, cannot draw card");
            }
            
        }
    }

    public void DeleteFromHand(Card card)
    {
        int remove_index = hand.IndexOf(card);

        hand.Remove(card);
        hand_game.RemoveAt(remove_index);
        //Arrange the hand after playing it
        Arrange();
    }
	
	public void discard_all(){
		while(hand.Count>0){
			Object.Destroy(hand[0].gameObject);			
			hand.RemoveAt(0);
			hand_game.RemoveAt(0);
		}
		Arrange();
	}
}
