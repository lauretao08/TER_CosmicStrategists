using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Hunter : Unit
{
    //========Pour le shader aparision======
    private Component[] my_meshRenderes;
    bool appear = true;
    bool disappear = false;
    Material myMaterial;
    float appearOverTime = 1.5f;
    private float speed = 0.75f;

    private void Start()
    {
        base.Start();
        //==================Initialisation du shader Aparision==============
        my_meshRenderes = GetComponentsInChildren(typeof(MeshRenderer));
        if (my_meshRenderes != null)
        {
            foreach (MeshRenderer m in my_meshRenderes)
            {
                m.material.SetFloat("Vector1_DCE1ED0", -2);
            }
        }
    }

    private void Update()
    {
        base.Update();

        //==================Changement de valeur pour le shader aparision==============
        if (appear)
        {
            appearOverTime += Time.deltaTime * speed;

            if (my_meshRenderes != null)
            {
                foreach (MeshRenderer m in my_meshRenderes)
                {
                    if (m.material.GetFloat("Vector1_DCE1ED0") > 2.5f) appearOverTime *= 1.1f;
                    m.material.SetFloat("Vector1_DCE1ED0", -2 + appearOverTime);
                    //Debug.Log(m.material.GetFloat("Vector1_A27884FF"));
                    if (m.material.GetFloat("Vector1_DCE1ED0") >= 50) appear = false;
                }
            }
        }
    }
    
    public override void start_turn_active()
    {
        
    }

    public override void on_arrival_active()
    {

    }

    public override void end_turn_active()
    {
        
    }

    public override void ActivateEffect()
    {
        Unit target = active_effect_target.GetComponent(typeof(Unit)) as Unit;
        target.damage(2);
        
    }

}
