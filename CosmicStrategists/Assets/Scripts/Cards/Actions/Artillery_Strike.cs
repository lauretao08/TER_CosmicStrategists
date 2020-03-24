using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery_Strike : Card_Action
{
	
    protected override void Activate()
    {
		game_manager.get_inactive_player().lose_hp(2);
    }

}
