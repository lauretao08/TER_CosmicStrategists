using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HighlightStyle : byte
{
    None,
    Highlight,
    Ready_To_Play,
	Not_Playable,
    Selected

}


abstract public class Card : MonoBehaviour
{
    //Parameters
    public string card_name; //name of the card
    public int card_base_energy_cost; //base energy cost of the card
    protected int card_energy_cost; //actual energy cost of the card
    
	[TextArea]
    public string card_description; // displayed description on the card

    public TextMesh name_component;
    public TextMesh description_component;
    public TextMesh cost_component;

    //Change these when changing highlight look
    private Color base_color;
    private Color highlight_color;
    private Color action_color;
    private Color inactive_color;

    protected MeshRenderer card_renderer;

    //used to keep in memory position in hand for drag and drop mechanics
    private Vector3 hand_position;
    private bool dragging;
    private float drag_distance;
    //used for highlighting
    private bool ready_to_play;


    protected Camera main_camera;
    protected CardPlayer card_manager;
	//For owner use get_owner()
    protected Game game_manager;

    // Start is called before the first frame update
    void Start()
    {
		card_energy_cost=card_base_energy_cost;
		
        name_component.text = card_name;
        description_component.text = card_description;
        cost_component.text = card_energy_cost.ToString();
        card_renderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        if (card_renderer == null)
        {
            Debug.Log("ERROR : NO CARD_RENDERER IN CARD");
        }
        main_camera = Camera.main;
        if (main_camera == null)
        {
            Debug.Log("ERROR : SCENE CAMERA HAS TO BE TAGGED AS MAIN");
        }
        base_color = Color.white;
        highlight_color = Color.blue;
        action_color = Color.green;
        inactive_color = Color.gray;

    }

    // Update is called once per frame
    //Update is necessary for drag and drop, unless Tao finds a better solution
    void Update()
    {
        if (dragging)
        {
            Ray ray = main_camera.ScreenPointToRay(Input.mousePosition);
            Vector3 ray_point = ray.GetPoint(drag_distance);
            transform.position = ray_point;

            //ATTENTION VALEUR EN DUR !! A MODIFIER ? VAR ENGIN ?
			if(ray_point.y >=45.0f){
				if(has_enough_energy()){
					ready_to_play = true;
					Highlight(HighlightStyle.Ready_To_Play);
				}else{
					Highlight(HighlightStyle.Not_Playable);
				}
			}else{
				ready_to_play = false;
                Highlight(HighlightStyle.Highlight);
			}
        }
    }


    //OnPlay is called when the card is played
    public void OnPlay()
    {
		Debug.Log("PLAYING CARD : " + card_name);
        get_owner().lose_energy(card_energy_cost);
		
        game_manager.display_feedback_card_played(get_owner(),card_name);
		
		//activate the card-specific effect
		Activate();
		
        //Delete the card in the hand before you delete it in game
        card_manager.DeleteFromHand(this);
        Object.Destroy(this.gameObject);
    }

    //Activate triggers thhe effect specific to the card, and is called on play
    abstract protected void Activate();
    virtual protected void OnStartTurn(){}
    virtual protected void OnEndTurn(){}
    

    //Set anchor position in hand for drag&drop
    public void SetHandPosition(Vector3 hand_position)
    {
        this.hand_position = hand_position;
    }

    void Highlight(HighlightStyle type)
    {
		switch(type){
		case HighlightStyle.None:
            card_renderer.material.color = base_color;
			break;
		case HighlightStyle.Highlight:
            card_renderer.material.color = highlight_color;
			break;
		case HighlightStyle.Ready_To_Play:
            card_renderer.material.color = action_color;
			break;
		case HighlightStyle.Not_Playable:
            card_renderer.material.color = inactive_color;
			break;
		default:
			Debug.Log("Invalid Highlight Type");
            card_renderer.material.color = base_color;
			break;
		}
    }

    void OnMouseEnter()
    {
        Highlight(HighlightStyle.Highlight);
    }

    void OnMouseExit()
    {
        Highlight(HighlightStyle.None);
    }

    void OnMouseDown()
    {
        drag_distance = Vector3.Distance(transform.position, main_camera.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        if (dragging && ready_to_play)
        {
            OnPlay();
        }
        dragging = false;
        transform.position = hand_position;
    }

    public void SetCardManager(CardPlayer card_manager)
    {
        this.card_manager = card_manager;
    }

	public bool has_enough_energy(){
		if(get_owner().get_energy()>=this.card_energy_cost){
			return true;
		}
		return false;
	}

    public void SetGame(Game game_manager)
    {
        this.game_manager = game_manager;
    }
	
	public Player get_owner(){
		return card_manager.player;
	}

}
