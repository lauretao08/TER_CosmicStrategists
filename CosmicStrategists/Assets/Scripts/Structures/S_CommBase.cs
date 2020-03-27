using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CommBase : Structure
{
	public override void start_turn(){
		game_manager.get_active_player().gain_hp(1);
	}
	
	public override void end_turn(){
	}
}	
