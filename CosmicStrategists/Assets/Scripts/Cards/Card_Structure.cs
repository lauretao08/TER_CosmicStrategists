using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Card_Structure : Card
{
    public GameObject spawned_unit;
	
	protected override void Activate()
    {
		Deploy();
	}
	
	protected void Deploy()
    {
        GameObject tmp = Instantiate(spawned_unit);
        if (this.get_owner().Equals(this.game_manager.playerA)){
            this.game_manager.board.add_structure_A_go(tmp);
        }else if (this.get_owner().Equals(this.game_manager.playerB)){
            this.game_manager.board.add_structure_B_go(tmp);
        }
    }
}
