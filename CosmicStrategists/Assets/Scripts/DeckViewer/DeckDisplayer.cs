using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDisplayer : MonoBehaviour
{
    public Camera main_camera;
    
    private List<int> deck_id;
    private List<Card> deck_card;
	private List<GameObject> deck_object;

    private DeckLoader deck_loader;

    //*Aspect ratio related parameters*//
    float screen_aspect;
    float cam_half_height;
    float cam_half_width;

    float card_distance;
    float card_offset_x;
    float card_offset_y;
    float camera_hFOV;


	public int cards_per_line;
	
	
	void Start(){
		main_camera = Camera.main;
        if (main_camera == null)
        {
            Debug.Log("ERROR : SCENE CAMERA HAS TO BE TAGGED AS MAIN");
        }
        deck_card = new List<Card>();
	
		deck_id = load_deck_from_file("Decks/placeholder.json");
		load_cards(deck_id);
		
		calculate_card_placement();
		Arrange();
	}
	
	
	void load_cards(List <int> deck_id){
		DeckLoader loader = new DeckLoader();
		foreach(int id in deck_id){
			GameObject tmp_go = Instantiate(loader.GenerateCardFromId(id), main_camera.transform.position, main_camera.transform.rotation);
			
			deck_card.Add(tmp_go.GetComponent<Card>());	
		}
	}
	
	
	void Arrange()
    {
        Vector3 base_pos = main_camera.transform.position;
        
		base_pos.z += card_distance;
		base_pos.x -= card_offset_x;
		base_pos.y += main_camera.transform.forward.y*card_distance;
		//VALEUR EN BRUT
		base_pos.x += 2.1f;
		
		int position_in_line = 0 ;
		
        foreach(Card c in deck_card)
        {
			c.SetHandPosition(base_pos);
            base_pos.x +=  2.1f;
			position_in_line++;
			if(position_in_line>cards_per_line){
				base_pos.y +=  3.1f;
				base_pos.x -=  position_in_line*2.1f;
				position_in_line=0;
			}
        }
    }	
	
	protected void calculate_card_placement(){
		float radAngle = main_camera.fieldOfView * Mathf.Deg2Rad;
        float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * main_camera.aspect);
        camera_hFOV = Mathf.Rad2Deg * radHFOV;

        screen_aspect = main_camera.aspect;
        card_distance = main_camera.focalLength / 6.0f;

        card_offset_x = Mathf.Tan((camera_hFOV / 2.0f)*Mathf.Deg2Rad) * card_distance;
        card_offset_y = Mathf.Tan((main_camera.fieldOfView/2.0f) * Mathf.Deg2Rad) * card_distance;
	}
	
	List<int> load_deck_from_file(string fileName){
		List<int> deck_return = new List<int>(); 
		DeckLoader loader = new DeckLoader();

        deck_return = loader.LoadFromFileInId(fileName);
		return deck_return;
	}
}