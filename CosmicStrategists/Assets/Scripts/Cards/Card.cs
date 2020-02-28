using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Change these when chaning highlight look
    private Color base_color;
    private Color highlight_color;
    private Color actionColor;

    private MeshRenderer card_renderer;

    //used to keep in memory position in hand for drag and drop mechanics
    private Vector3 hand_position;
    private bool dragging;
    private float drag_distance;

    private Camera main_camera;

    // Start is called before the first frame update
    void Start()
    {
        name_component.text = card_name;
        description_component.text = card_description;
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
        actionColor = Color.red;
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
        }
    }

    //OnPlay is called when the card is played
    void OnPlay()
    {

    }

    //Set anchor position in hand for drag&drop
    public void SetHandPosition(Vector3 hand_position)
    {
        this.hand_position = hand_position;
    }

    void Highlight(bool on)
    {
        if (on)
        {
            card_renderer.material.color = highlight_color;
        }
        else
        {
            card_renderer.material.color = base_color;
        }
    }

    void OnMouseEnter()
    {
        Highlight(true);
    }

    void OnMouseExit()
    {
        Highlight(false);
    }

    void OnMouseDown()
    {
        drag_distance = Vector3.Distance(transform.position, main_camera.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        transform.position = hand_position;
    }

}
