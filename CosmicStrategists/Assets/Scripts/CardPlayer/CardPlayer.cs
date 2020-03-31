using System.Collections;
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
    public List<Card> hand; //J'ai modifier ca a l'arrache
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

        deck = deck_loader.LoadFromFileInId("Assets/Resources/Decks/placeholder.json");
        
        ShuffleDeck();

        draw_pile = new List<int>(deck);

        /**ASPECT RATIO AND CARD PLACEMENT, this has to be calculated once again upon resolution or fov changes**/
        float radAngle = camera_player.fieldOfView * Mathf.Deg2Rad;
        float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * camera_player.aspect);
        camera_hFOV = Mathf.Rad2Deg * radHFOV;

        screen_aspect = camera_player.aspect;
        card_distance = camera_player.focalLength / 6.0f;

        card_offset_x = Mathf.Tan((camera_hFOV / 2.0f)*Mathf.Deg2Rad) * card_distance;
        card_offset_y = Mathf.Tan((camera_player.fieldOfView/2.0f) * Mathf.Deg2Rad) * card_distance;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Arrange()
    {
        /**ASPECT RATIO**/
        /*screen_aspect = (float)Screen.width / (float)Screen.height;
        cam_half_height = camera_player.orthographicSize;
        cam_half_width = screen_aspect * cam_half_height;*/

        //ATTENTION : PAS DE VALEURS EN DUR
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
        int tmp = 0;
        Card tmp_card;
        //Card tmp_card = new Card();
        GameObject tmp_go;

        for (int i = 0; i < nb; i++)
        {
			if (hand.Count >= max_number_cards_hand){
                Debug.Log("Hand full ");
				return;
			}
			
            if (draw_pile.Count < 1){
				
                Debug.Log("Empty draw pile, cannot draw card");
            }else{	
                tmp = draw_pile[0];
                draw_pile.RemoveAt(0);
                //hand.Add(tmp);
                Vector3 card_pos = camera_player.transform.position;


                GameObject CardTmp;
                //GameObject CardTmp = new GameObject();
                CardTmp = deck_loader.GenerateCardFromId(tmp);

                tmp_go = Instantiate(deck_loader.GenerateCardFromId(tmp), card_pos, camera_player.transform.rotation);
                hand_game.Add(tmp_go);

                //OPTIMIZE THIS GETCOMPONENT!
                tmp_card = tmp_go.GetComponent(typeof(Card)) as Card;
                tmp_card.SetCardManager(this);
                tmp_card.SetGame(get_current_game());
                hand.Add(tmp_card);
                //arrange card position upon drawing
                Arrange();
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
	
	public Game get_current_game(){
		return player.current_game;
	}

}
