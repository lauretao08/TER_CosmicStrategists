using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*///	Attributes  ///
	
	bool DEBUG_PRINT		//enable/disable logs in debug console
	bool SUSPICIOUS_WARNING	//enable/disable logs for strange behaviors

	int STARTING_HEALTH 	//Constant starting Health (Move it ?)
	int MAX_ENERGY_CAP		//Constant cap of max energy (Move it ?)
	
	int health		// Current Health
	int energy		// Current Energy
	int max_energy	// Max Energy available
	
	bool is_dead	//Player status Alive/Dead
	
*/////////////////////


/*///	 Methods	///

	void Start 		//Unused
	void Update		//Unused
	
	void init		//Initialize energy,max_energy,health and cards stuff
	
	void start_turn	//Increments max_energy refills energy and draw a cards
	void end_turn	//Currently useless
	
	void do_action	//Not Yet Implemented
	
	int get_health		//Health getter, returns health
	int get_energy		//Energy getter, returns energy
	int get_max_energy	//Max_Energy getter, returns max_energy
	
	int gain_hp(int)	//Gain inputted health, returns health
	int lose_hp(int)	//Lose inputted health, returns health, if health should become negative replaces it by 0 and player dies
	
	int gain_energy(int)	//Gain inputted energy, returns energy
	int lose_energy(int)	//Lose inputted energy, returns energy, if energy negative Log an Error
	int refill_energy		//Set energy to max_energy, returns energy
	
	int gain_max_energy(int)	//Gain inputted max_energy, returns max_energy
	int lose_max_energy(int)	//Lose inputted max_energy, returns max_energy, if max_energy negative Log an Error
	int increment_max_energy	//Increment max_energy, returns max_energy
	
	bool dies;		//Set player as dead
	
	bool draw_card		//Draw a card, returns true if draw was successful
	bool draw_card(int)	//Draw inputted number of card, returns true if all draws were successful, if input negative Log an Error 
	
*//////////////////////


/*///	TODO	///


*//////////////////

public class Player : MonoBehaviour
{
	public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
	public static int STARTING_HEALTH=20;
	public static int MAX_ENERGY_CAP=10;
	
	
	public Text UI_Health;
	public Text UI_Energy;
	
	
	public int health;
	public int energy;
	private int max_energy;
	
	public bool is_dead;
	
	/*
	private List<Card>  deck;
	private List<Card>  graveyard;
	private List<Card>	hand;
	*/
	
    // Start is called before the first frame update
    void Start()
    {
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
		
		max_energy=0;
		energy=0;
	
	
		/*
		deck=new List<Card>;
		hand=new List<Card>;
		graveyard=new List<Card>;
		*/
		
	}
	
	public void start_turn()
	{
		if(DEBUG_PRINT){Debug.Log("["+this+".start_turn()] Starting turn...");}
		
		increment_max_energy();
		refill_energy();
		draw_card();
	}
	
	public void end_turn()
	{
		if(DEBUG_PRINT){Debug.Log("["+this+".end_turn()] Ending turn...");}
		
	}
	
	
	public void do_action()
	{
		
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
	

//Card Management

	public bool draw_card(){
		//Pioche effective
		if(DEBUG_PRINT){Debug.Log("["+this+".draw_card()] Card drawn");}
		return true; //Retourne si la pioche est réussie
	}
	
	public bool draw_card(int n){
		
		if(n<=0){
			Debug.Log("ERROR : ["+this+".draw_card("+n+")] Negative Draw ");
		}
		
		if(SUSPICIOUS_WARNING){
			if(n==1){
				Debug.Log("WARNING : ["+this+".draw_card("+n+")]  Only one card drawn! maybe consider using "+this+"draw_card()");
			}
		}
		
		bool res = true;
		for(int i =0;i<n;i++){
			res= res & draw_card();
		}
		return res;
	}
	
	
	
	
}
