using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Unit : Unit
{
	
	
	public override void start_turn(){
		Debug.Log("BaseU st");
	}	
	
	public override void end_turn(){
		Debug.Log("BaseU et");
	}
}	