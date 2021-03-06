﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public Camera camera_player;
    public GameObject card_prefab;
	public Player player;
	//For Game use get_current_game()

    private List<int> deck;
    private List<int> draw_pile;
    public List<Card> hand; //J'ai modifier ca a l'arrache pour le comportement ennemi
    private List<GameObject> hand_game;

    private DeckLoader deck_loader;

    //*Aspect ratio related parameters*//
    float screen_aspect;
    float cam_half_height;
    float cam_half_width;

    float card_distance;
    float card_offset_x;
    float card_offset_y;
    float camera_hFOV;

    public int max_number_cards_hand;

    private float card_offset_in_hand=2.1f;


    //shuffle the deck
    public void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        deck = new List<int>();
        
        hand = new List<Card>();
        hand_game = new List<GameObject>();

        deck_loader = new DeckLoader();

        deck = deck_loader.LoadFromFileInId("Decks/placeholder.json");
        
        ShuffleDeck();

        draw_pile = new List<int>(deck);

        /**ASPECT RATIO AND CARD PLACEMENT, this has to be calculated once again upon resolution or fov changes**/
        calculate_card_placement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Arrange()
    {
        Vector3 base_pos = camera_player.transform.position;
        
		if(player.is_human()){
			base_pos.z += card_distance;
			base_pos.x -= card_offset_x;
			base_pos.y += camera_player.transform.forward.y*card_distance;
			base_pos.y -= (card_offset_y);
		}else{
			
			base_pos.z -= card_distance*3; //POUR NE PAS VOIR LES CARTES ADVERSES
		}
		
        Card tmp_card;
        foreach(Card c in hand)
        {
            c.SetHandPosition(base_pos);
            base_pos.x += card_offset_in_hand;
        }
		
		/*
        Card tmp_card;
        foreach(GameObject c in hand_game)
        {
            c.transform.position = base_pos;
            //ATTENTION : AMELIORATION NECESSAIRE !
            tmp_card = c.GetComponent<Card>();
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
		*/
    }	

	public void calculate_card_placement(){
		float radAngle = camera_player.fieldOfView * Mathf.Deg2Rad;
		float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * camera_player.aspect);
		camera_hFOV = Mathf.Rad2Deg * radHFOV;

		screen_aspect = camera_player.aspect;
		card_distance = camera_player.focalLength / 6.0f;

		card_offset_x = Mathf.Tan((camera_hFOV / 2.0f)*Mathf.Deg2Rad) * card_distance;
		card_offset_y = Mathf.Tan((camera_player.fieldOfView/2.0f) * Mathf.Deg2Rad) * card_distance;

	}

    public Card Draw(int nb)
    {
        int tmp = 0;
        Card tmp_card = null;
        GameObject tmp_go;

        for (int i = 0; i < nb; i++)
        {
			if (hand.Count >= max_number_cards_hand){
                Debug.Log("Hand full ");
				return null;
			}
			
            if (draw_pile.Count < 1){
				
                Debug.Log("Empty draw pile, cannot draw card");
				return null;
            }else{	
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                //hand.Add(tmp);
                Vector3 card_pos = camera_player.transform.position;
				
                tmp_go = Instantiate(deck_loader.GenerateCardFromId(tmp), card_pos, camera_player.transform.rotation);
                hand_game.Add(tmp_go);

                //OPTIMIZE THIS GETCOMPONENT!
                tmp_card = tmp_go.GetComponent<Card>();
                tmp_card.SetCardManager(this);
                tmp_card.SetGame(get_current_game());
                hand.Add(tmp_card);
                //arrange card position upon drawing
                Arrange();
            }
        }

		return tmp_card;
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
	
	public Game get_current_game(){
		return player.current_game;
	}

	public bool refuse(){
		//Returns true if not my turn
		return get_current_game().get_id_active_player();
	}
}
