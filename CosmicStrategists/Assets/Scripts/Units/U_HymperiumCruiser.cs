using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_HymperiumCruiser : Unit
{
	public override void start_turn(){
		game_manager.get_inactive_player().lose_hp(4);
	}
	
	public override void end_turn(){
	}
}	
