using System.Collections;
using System.Collections.Generic;
using UnityEngine.ParticleSystemJobs;
using UnityEngine;

public class Artillery_Strike : Card_Action
{
    
   
    protected override void Activate()
    {
        if (game_manager.get_id_active_player() == false)
        { //Si joueur A	
            this.game_manager.board._as.active = true;
        }
        get_opponent().lose_hp(2);
    }

}
