using System.Collections;
using System.Collections.Generic;
using UnityEngine.ParticleSystemJobs;
using UnityEngine;

public class Artillery_Strike : Card_Action
{
    
   
    protected override void Activate()
    {

        this.game_manager.board._as.active = true;
        
        get_opponent().lose_hp(2);
    }

}
