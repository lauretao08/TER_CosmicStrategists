using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_OutlawDriver : Unit
{
    private Unit buffed_unit;
    public int buff_amount = 2;

    //========Pour le shader aparision======
    private Component[] my_meshRenderes;
    bool appear = true;
    Material myMaterial;
    float appearOverTime = 1.0f;
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
                m.material.SetFloat("Vector1_4004EA48", -2);
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
                    if (m.material.GetFloat("Vector1_4004EA48") > 2.5f) appearOverTime *= 1.05f;
                    m.material.SetFloat("Vector1_4004EA48", -2 + appearOverTime);
                    //Debug.Log(m.material.GetFloat("Vector1_A27884FF"));
                    if (m.material.GetFloat("Vector1_4004EA48") >= 100)
                    {
                        appear = false;
                        appearOverTime = 1.0f;
                    }
                }
            }



        }


        if (disappear)
        {

            appearOverTime += 4 * Time.deltaTime * speed;

            if (my_meshRenderes != null)
            {
                foreach (MeshRenderer m in my_meshRenderes)
                {

                    m.material.SetFloat("Vector1_4004EA48", 10 - appearOverTime);
                    Debug.Log(m.material.GetFloat("Vector1_4004EA48"));
                    if (m.material.GetFloat("Vector1_4004EA48") <= -2) detuit_shader_fini = true;
                }
            }
        }
    }
    public override void start_turn_active()
    {
        //debuff unit buffed last turn on turn start
        if (buffed_unit != null)
        {
            buffed_unit.change_damage(buffed_unit.get_damage() - buff_amount);
            buffed_unit = null;
        }
        
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
        buffed_unit = target;
        target.change_damage(target.get_damage() + buff_amount);

    }


}
