using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Structure : MonoBehaviour
{
    public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
    protected Game game_manager;
	
	public virtual void start_turn(){}
	public virtual void on_arrival(){}
	public virtual void end_turn(){}
	
	void Start(){	
		deploy();
	}
	
	public void check_state(){
		
	}
	
	public void deploy(){
		on_arrival();
	}
	
	public void set_game_manager(Game gm){
		game_manager=gm;
	}
}
