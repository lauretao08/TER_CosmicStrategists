using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    public bool DEBUG_PRINT;
    public bool SUSPICIOUS_WARNING;

    protected Game game_manager;

    public int health;
    public int max_health;

    public TextMeshPro HPText;

    /**active effects**/
    public bool has_active_effect;
    public bool self_targetable = false;
    protected GameObject active_effect_target;
    private bool dragging = false;
    private bool selected = false;
    protected bool right_turn = false;
    protected bool deactivated = false;

    private Camera main_camera;

    

    private Color base_color;
    private Color highlight_color;
    private Color action_color;
    private Color selected_color;
    private Color unusable_color;

    private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;

    public Card origin_card;

    //============Shader===============
    private MeshRenderer unit_renderer;

    protected bool disappear;
    protected bool detuit_shader_fini;

    //============VFX============
    public HighLight    Aura_HighLight;
    public Aura_Skill   Aura_Skill;
    public ElectricHit  Hit;
    public Explosion    Explosion_cartoon;

    private int compteur_hit = 0;



    //these methods will be overriden by non-activable units. others will use the activable methods below
    public virtual void start_turn()
    {
        if (has_active_effect)
        {
            if(game_manager.get_active_player() == game_manager.playerA)
            {
                right_turn = true;
                Highlight(HighlightStyle.Highlight);
            }
            else
            {
                right_turn = false;
            }

            //Highlight(HighlightStyle.Highlight);
            deactivated = false;
            start_turn_active();
        }
    }

    public virtual void on_arrival()
    {
        if (has_active_effect)
        {
            if (game_manager.get_active_player() == game_manager.playerA)
            {
                right_turn = true;
                Highlight(HighlightStyle.Highlight);
            }
            else
            {
                right_turn = false;
            }

            //Highlight(HighlightStyle.Highlight);
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

    protected void Start()
    {
        //=============================================

        Aura_HighLight = GetComponentInChildren(typeof(HighLight)) as HighLight;
        Aura_Skill = GetComponentInChildren(typeof(Aura_Skill)) as Aura_Skill;
        Hit = GetComponentInChildren(typeof(ElectricHit)) as ElectricHit;
        Explosion_cartoon = GetComponentInChildren(typeof(Explosion)) as Explosion;
        //===============================================
        unit_renderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        disappear = false;


        

        cursorTexture = Resources.Load<Texture2D>("Textures/Crosshair1");
        main_camera = Camera.main;
        //*will be changed*//
        base_color = Color.white;
        highlight_color = Color.red;
        action_color = Color.green;
        selected_color = Color.magenta;
        unusable_color = Color.gray;

        HPText.text = health + "/" + max_health;

        deploy();
    }

  

    public void RotateHPText()
    {
        HPText.transform.Rotate(0.0f, 180.0f, 0.0f);
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
        detuit_shader_fini = false;  //pour détuire l'objet après le shader soit fini
        if (damage_number > 0)
        {
            Hit.active = true;
            health -= damage_number;
            HPText.text = health + "/" + max_health;
        }
        else
        {
            Debug.Log("WARNING : Damage value negative");
        }
        
        if (health <= 0)
        {
            Explosion_cartoon.active = true;
            disappear = true;

        }
    }

    //**Active effects functions**//

    protected void Update()
    {
        if(Hit.active == true)
            compteur_hit++;
        if (compteur_hit > 1000)
            Hit.active = false;

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
        
        if (detuit_shader_fini)
        {
            game_manager.activate_feedback_unit(true);
            this.game_manager.board.removeUnitFromPlayer(this, this.gameObject);
            Destroy(this.gameObject);
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
            //=====VFX=====
            Aura_Skill.active = true;
        }
        if (!right_turn)
        {
            Highlight(HighlightStyle.Selected);
        }
        game_manager.activate_feedback_unit(true);
        game_manager.display_feedback_unit(origin_card);

        //=======VFX==========
        Aura_HighLight.active = true;
        

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

        game_manager.activate_feedback_unit(false);
        Aura_HighLight.active = false;
        if(Aura_Skill!=null)
            Aura_Skill.active = false;
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