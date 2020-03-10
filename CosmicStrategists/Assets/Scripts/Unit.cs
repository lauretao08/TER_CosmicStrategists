using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*///	Attributes  ///
	
	bool DEBUG_PRINT		//enable/disable logs in debug console
	bool SUSPICIOUS_WARNING	//enable/disable logs for strange behaviors

	int health;
	int health_max;
	
*/////////////////////


/*///	 Methods	///

	Start		//Unused
	Update		//Unused
	
	check_state		//NYI, check if units has no health and need to be destroyed
	
	deploy			//When the unit enter the board
	destroy			//Whe the unit is destroyed 
	
	
	

*//////////////////////

/*///	TODO	///

	deploy
	destroy

	check_state
	
	Basic actions :
		Shoot
		Heal
		


*//////////////////

public class Unit : MonoBehaviour
{
	public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
	public int health;
	public int max_health;
	
	void Start(){	}
	void Update(){	}
	
	public void start_turn()
	{
		
	}
	
	public void end_turn()
	{
		
	}
	
	public void check_state(){
		
	}
	
	public void deploy(){
		health=max_health;
		
	}
}
