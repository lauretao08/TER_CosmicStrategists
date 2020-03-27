using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Scout : Unit
{
	public override void start_turn(){
	
	}
	
	public override void end_turn(){
		game_manager.get_inactive_player().lose_hp(1);
	}
}	
