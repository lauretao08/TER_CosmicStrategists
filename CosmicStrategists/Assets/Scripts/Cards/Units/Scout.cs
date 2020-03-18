﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Card_Unit
{

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    protected override void Activate()
    {
        GameObject tmp = Instantiate(spawned_unit);
        if (this.get_owner().Equals(this.game_manager.playerA))
        {
            this.game_manager.board.add_unit_A_go(tmp);
        }else if (this.get_owner().Equals(this.game_manager.playerB))
        {
            this.game_manager.board.add_unit_B_go(tmp);
        }

    }
}
