using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CommBase : Structure
{
    //========Pour le shader aparision======
    private Component[] my_meshRenderes;
    bool appear = true;
    bool disappear = false;
    Material myMaterial;
    float appearOverTime = 1.0f;
    private float speed = 0.85f;

    private void Start()
    {
        base.Start();
        //==================Initialisation du shader Aparision==============
        my_meshRenderes = GetComponentsInChildren(typeof(MeshRenderer));
        if (my_meshRenderes != null)
        {
            foreach (MeshRenderer m in my_meshRenderes)
            {
                m.material.SetFloat("Vector1_672D53BB", -2);
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
                    if (m.material.GetFloat("Vector1_672D53BB") > 5.0f) appearOverTime *= 1.05f;
                    m.material.SetFloat("Vector1_672D53BB", -2 + appearOverTime);
                    if (m.material.GetFloat("Vector1_672D53BB") >= 200) appear = false;
                }
            }
        }



    }

    public override void start_turn(){
		game_manager.get_active_player().gain_hp(1);
	}
	
	public override void end_turn(){
	}
}	
