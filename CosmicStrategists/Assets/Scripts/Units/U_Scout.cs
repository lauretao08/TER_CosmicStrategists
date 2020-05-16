using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Scout : Unit
{
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
                m.material.SetFloat("Vector1_C5620752", -2);
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
                    if (m.material.GetFloat("Vector1_C5620752") > 2.5f) appearOverTime *= 1.05f;
                    m.material.SetFloat("Vector1_C5620752", -2 + appearOverTime);
                    //Debug.Log(m.material.GetFloat("Vector1_A27884FF"));
                    if (m.material.GetFloat("Vector1_C5620752") >= 100)
                    {
                        appear = false;
                        appearOverTime = 1.0f;
                    }
                }
            }

            

        }

       
        if (disappear)
        {

            appearOverTime += 4*Time.deltaTime * speed;

            if (my_meshRenderes != null)
            {
                foreach (MeshRenderer m in my_meshRenderes)
                {

                    m.material.SetFloat("Vector1_C5620752", 10 - appearOverTime);
                    //Debug.Log(m.material.GetFloat("Vector1_A27884FF"));
                    if (m.material.GetFloat("Vector1_C5620752") <= -2) detuit_shader_fini = true;
                }
            }
        }
    }
    

    public override void start_turn(){

	}

    public override void on_arrival()
    {

    }

    public override void end_turn(){
		game_manager.get_inactive_player().lose_hp(1);
    }
}
