using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*///	TODO	///


*//////////////////

public class Player : MonoBehaviour
{
	//Constants
	public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
	public int STARTING_HEALTH;
	public int STARTING_ENERGY;
	public int MAX_ENERGY_CAP;
	
	
	//Enum
	
	public enum P_type : byte
	{
		HUMAN,
		ROBOT
	}
	
	
	//Objects
	public Game current_game;
	
    public GameObject EventManager;
	private CardPlayer card_controller;
	
	public Text UI_Health;
	public Text UI_Energy;
	
	//Variables
	public P_type player_type;
	
	public int health;
	public int energy;
	private int max_energy;
	
	private bool is_dead;
	
	
    // Start is called before the first frame update
    void Start()
    {
		card_controller = EventManager.GetComponent(typeof(CardPlayer)) as CardPlayer;
		if(card_controller == null){
			Debug.Log("ERROR : Card_Controller not Found");
		}
		
		init();
    }

    // Update is called once per frame
    void Update()
    {    
		UI_Energy.text=""+energy;
		UI_Health.text=""+health;
    }
	
	public void init()
	{
		health = STARTING_HEALTH;
		is_dead= false;
		
		max_energy=STARTING_ENERGY;
		energy=max_energy;
		
	}
	
	public void start_turn()
	{
		if(DEBUG_PRINT){Debug.Log("["+this+".start_turn()] Starting turn...");}
		
		increment_max_energy();
		refill_energy();
		card_controller.Draw(1);
	}
	
	public void end_turn()
	{
		if(DEBUG_PRINT){Debug.Log("["+this+".end_turn()] Ending turn...");}
		
	}
	
	
//Getters
	public int get_health	 (){	return this.health;}
	public int get_energy	 (){	return this.energy;}
	public int get_max_energy(){	return this.max_energy;}
	
	
//Health modification 
	
	public int gain_hp(int h){
		health=health+h;
		
		if(DEBUG_PRINT){Debug.Log("["+this+".gain_hp("+h+")] Health ="+this.health);}
		
		return health;
	}
	
	public int lose_hp(int h){
		health=health-h;
		if(health <= 0){
			dies();
			health=0;
		}
		
		if(DEBUG_PRINT){Debug.Log("["+this+".lose_hp("+h+")] Health ="+this.health);}
		
		return health;
	}

//Energy modification 
		
	public int gain_energy(int e){
		energy=energy+e;
		
		if(DEBUG_PRINT){Debug.Log("["+this+".gain_energy("+e+")] Energy ="+this.energy);}
		
		if(SUSPICIOUS_WARNING){
			if(energy>max_energy){
				Debug.Log("WARNING : ["+this+".gain_energy("+e+")] Energy > max_energy ("+this.energy+">"+this.max_energy+")");
			}
		}
		
		return energy;
	}
	
	public int lose_energy(int e){
		energy=energy-e;
		
		if(energy < 0){
			Debug.Log("ERROR : ["+this+".lose_energy("+e+")] Negative Energy ("+this.energy+")");
		}
		
		if(DEBUG_PRINT){Debug.Log("["+this+".lose_energy("+e+")] Energy ="+this.energy);}
		
		return energy;
	}
	
	public int refill_energy(){
		energy=max_energy;
		
		if(DEBUG_PRINT){Debug.Log("["+this+".refill_energy()] Energy ="+this.energy);}
		
		return energy;
	}
	
//Max_Energy modification

	public int gain_max_energy(int e){
		max_energy=max_energy+e;
		
		if(DEBUG_PRINT){Debug.Log("["+this+".gain_max_energy("+e+")] Max_Energy ="+this.max_energy);}
		
		return max_energy;
	}
	
	public int lose_max_energy(int e){
		max_energy=max_energy-e;
		
		if(max_energy < 0){
			Debug.Log("ERROR : ["+this+".lose_max_energy("+e+")] Negative Max_Energy ("+this.max_energy+")");
		}
		
		if(DEBUG_PRINT){Debug.Log("["+this+".lose_max_energy("+e+")] Max_Energy ="+this.max_energy);}
		
		return max_energy;
	}
	
	public int increment_max_energy(){
		
		if(max_energy<MAX_ENERGY_CAP){
			max_energy++;
		}
		
		if(DEBUG_PRINT){Debug.Log("["+this+".increment_max_energy()] Max_Energy ="+this.max_energy);}
		
		return max_energy;
	}
	
//NO IDEA HOW TO NAME THIS !

	public bool dies(){
		is_dead=true;
		return is_dead;
	}
	
	public void auto_turn(){
		System.Random rng = new System.Random();
		int n = card_controller.hand.Count;
        while (n >= 1)
        {
			n--;
			int k = rng.Next(n + 1);
			if(card_controller.hand[k].has_enough_energy()){
				card_controller.hand[k].OnPlay();
			}else{
				break;
			}            
        }
		current_game.finish_turn();
	}
	
	public CardPlayer get_card_controller(){
		return card_controller;
	}
	
	public bool is_human(){
		return player_type == P_type.HUMAN;
	}
	
	public bool is_robot(){
		return player_type == P_type.ROBOT;
	}
}
