using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HighlightStyle
{
    None,
    Highlight,
    Ready_To_Play

}


public class Card : MonoBehaviour
{
    //Parameters
    public string card_name="-"; //name of the card
    public int card_base_energy_cost=0; //base energy cost of the card
    private int card_energy_cost = 0; //actual energy cost of the card
    [TextArea]
    public string card_description="-"; // displayed description on the card

    public TextMesh name_component;
    public TextMesh description_component;
    public TextMesh cost_component;

    //Change these when chaning highlight look
    private Color base_color;
    private Color highlight_color;
    private Color action_color;
    private Color inactive_color;

    private MeshRenderer card_renderer;

    //used to keep in memory position in hand for drag and drop mechanics
    private Vector3 hand_position;
    private bool dragging;
    private float drag_distance;
    //used for highlighting
    private bool ready_to_play;

    private Camera main_camera;

    private CardPlayer card_manager;

    // Start is called before the first frame update
    void Start()
    {
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
            //Optimize action highlighting ?
            if(!ready_to_play && ray_point.y >= 1.0f)
            {
                ready_to_play = true;
                Highlight(HighlightStyle.Ready_To_Play);
            }
            if(ready_to_play && ray_point.y < 1.0f)
            {
                ready_to_play = false;
                Highlight(HighlightStyle.None);
            }
        }
    }

    //OnPlay is called when the card is played
    void OnPlay()
    {
        Debug.Log("PLAYING CARD : " + card_name);
        //Delete the card in the hand before you delete it in game
        card_manager.DeleteFromHand(this);
        Object.Destroy(this.gameObject);
    }

    //Set anchor position in hand for drag&drop
    public void SetHandPosition(Vector3 hand_position)
    {
        this.hand_position = hand_position;
    }

    void Highlight(HighlightStyle type)
    {
        if (type== HighlightStyle.Highlight)
        {
            card_renderer.material.color = highlight_color;
        }
        else if (type == HighlightStyle.Ready_To_Play)
        {
            card_renderer.material.color = action_color;
        }
        else
        {
            card_renderer.material.color = base_color;
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

}
