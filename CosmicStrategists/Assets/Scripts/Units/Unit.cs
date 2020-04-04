using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    public bool DEBUG_PRINT;
    public bool SUSPICIOUS_WARNING;

    protected Game game_manager;

    public int health;
    public int max_health;

    /**active effects**/
    public bool has_active_effect;
    public bool self_targetable = false;
    protected GameObject active_effect_target;
    private bool dragging = false;
    private bool selected = false;
    protected bool right_turn = false;
    protected bool deactivated = false;

    private Camera main_camera;

    private MeshRenderer unit_renderer;

    private Color base_color;
    private Color highlight_color;
    private Color action_color;
    private Color selected_color;
    private Color unusable_color;

    private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;


    //these methods will be overriden by non-activable units. others will use the activable methods below
    public virtual void start_turn()
    {
        if (has_active_effect)
        {
            right_turn = true;
            Highlight(HighlightStyle.Highlight);
            deactivated = false;
            start_turn_active();
        }
    }

    public virtual void on_arrival()
    {
        if (has_active_effect)
        {
            right_turn = true;
            Highlight(HighlightStyle.Highlight);
            on_arrival_active();
        }
    }
    public virtual void end_turn()
    {
        if (has_active_effect)
        {
            right_turn = false;
            Highlight(HighlightStyle.None);
            end_turn_active();
        }
    }


    public virtual void start_turn_active() { }
    public virtual void on_arrival_active() { }
    public virtual void end_turn_active() { }

    void Start()
    {

        unit_renderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;

        cursorTexture = Resources.Load<Texture2D>("Textures/Crosshair1");
        main_camera = Camera.main;
        //*will be changed*//
        base_color = Color.white;
        highlight_color = Color.red;
        action_color = Color.green;
        selected_color = Color.magenta;
        unusable_color = Color.gray;



        deploy();
    }

    public void check_state()
    {

    }

    public void deploy()
    {
        health = max_health;
        on_arrival();
    }

    public void set_game_manager(Game gm)
    {
        game_manager = gm;
    }

    public virtual void damage(int damage_number)
    {
        if (damage_number > 0)
        {
            health -= damage_number;
        }
        else
        {
            Debug.Log("WARNING : Damage value negative");
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
            this.game_manager.board.removeUnitFromPlayer(this, this.gameObject);
        }
    }

    //**Active effects functions**//

    void Update()
    {
        if (right_turn)
        {
            if (selected && dragging)
            {
                Ray ray = main_camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit))
                {
                    if (DEBUG_PRINT) { Debug.Log("RAY DETECTED : " + hit.collider.gameObject.name); }
                    active_effect_target = hit.collider.gameObject;

                }
            }
        }

    }


    public void Highlight(HighlightStyle type)
    {
        switch (type)
        {
            case HighlightStyle.None:
                unit_renderer.material.SetColor("_BaseColor", base_color);
                break;
            case HighlightStyle.Highlight:
                unit_renderer.material.SetColor("_BaseColor", highlight_color);
                break;
            case HighlightStyle.Ready_To_Play:
                unit_renderer.material.SetColor("_BaseColor", action_color);
                break;
            case HighlightStyle.Selected:
                unit_renderer.material.SetColor("_BaseColor", selected_color);
                break;
            case HighlightStyle.Not_Playable:
                unit_renderer.material.SetColor("_BaseColor", unusable_color);
                break;
            default:
                Debug.Log("Invalid Highlight Type for unit");
                unit_renderer.material.color = base_color;
                break;
        }
    }


    public void OnMouseEnter()
    {
        if (has_active_effect && right_turn && !deactivated)
        {
            selected = true;
            Highlight(HighlightStyle.Ready_To_Play);
        }
        if (!right_turn)
        {
            Highlight(HighlightStyle.Selected);
        }
    }

    public void OnMouseExit()
    {
        if (has_active_effect && right_turn && !deactivated)
        {
            if (!dragging)
            {
                selected = false;
                Highlight(HighlightStyle.Highlight);
            }
        }
        if (!right_turn)
        {
            Highlight(HighlightStyle.None);
        }
    }

    public void OnMouseDown()
    {
        if (has_active_effect && right_turn)
        {
            dragging = true;
            if (selected)
            {
                Cursor.SetCursor(cursorTexture, new Vector2(256, 256), cursorMode);
            }
        }
    }

    public void OnMouseUp()
    {
        if (has_active_effect && right_turn)
        {

            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            if (selected)
            {
                selected = false;
                Highlight(HighlightStyle.Highlight);
                //activate effect
                if (dragging && active_effect_target.GetComponent(typeof(Unit)) != null)
                {
                    //do not activate effect if can't target self and target is self
                    if (!self_targetable && active_effect_target == this.gameObject)
                    {
                        Debug.Log("Unit " + this.name + " Cannot target self");
                    }
                    else
                    {
                        ActivateEffect();
                        Highlight(HighlightStyle.Not_Playable);
                        //clear target after effect resolves
                        active_effect_target = null;
                        deactivated = true;
                    }

                }
            }

            /*if (dragging && ready_to_activate)
            {
                ActivateEffect();
            }*/
            dragging = false;
        }
    }

    public virtual void ActivateEffect() { }

}