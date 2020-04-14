using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Structure : MonoBehaviour
{
    public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
    protected Game game_manager;

    public Card origin_card;

    private MeshRenderer structure_renderer;
    private Color base_color;
    private Color highlight_color;
    private Color action_color;
    private Color selected_color;
    private Color unusable_color;


    //=========VFX============
    public HighLight Aura_HighLight;


    public virtual void start_turn(){}
	public virtual void on_arrival(){}
	public virtual void end_turn(){}
	
	protected void Start(){

        structure_renderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        Aura_HighLight = GetComponentInChildren(typeof(HighLight)) as HighLight;

        //*will be changed*//
        base_color = Color.white;
        highlight_color = Color.red;
        action_color = Color.green;
        selected_color = Color.magenta;
        unusable_color = Color.gray;



        deploy();
	}
    protected void Update()
    {
        
    }
    public void check_state(){
		
	}
	
	public void deploy(){
		on_arrival();
	}
	
	public void set_game_manager(Game gm){
		game_manager=gm;
	}

    //highlighting and feecback
    public void Highlight(HighlightStyle type)
    {
        switch (type)
        {
            case HighlightStyle.None:
                structure_renderer.material.SetColor("_BaseColor", base_color);
                break;
            case HighlightStyle.Highlight:
                structure_renderer.material.SetColor("_BaseColor", highlight_color);
                break;
            case HighlightStyle.Ready_To_Play:
                structure_renderer.material.SetColor("_BaseColor", action_color);
                break;
            case HighlightStyle.Selected:
                structure_renderer.material.SetColor("_BaseColor", selected_color);
                break;
            case HighlightStyle.Not_Playable:
                structure_renderer.material.SetColor("_BaseColor", unusable_color);
                break;
            default:
                Debug.Log("Invalid Highlight Type for unit");
                structure_renderer.material.color = base_color;
                break;
        }
    }

    public void OnMouseEnter()
    {
        Highlight(HighlightStyle.Selected);
        game_manager.activate_feedback_unit(true);
        game_manager.display_feedback_unit(origin_card);
        Aura_HighLight.active = true;

    }

    public void OnMouseExit()
    {
        Highlight(HighlightStyle.None);

        game_manager.activate_feedback_unit(false);
        Aura_HighLight.active = false;
    }

}
