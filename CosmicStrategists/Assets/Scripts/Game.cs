using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*///	Attributes  ///
	
	bool DEBUG_PRINT		//enable/disable logs in debug console
	bool SUSPICIOUS_WARNING	//enable/disable logs for strange behaviors

	enum G_state game_state //Status of the game [NOT_STARTED/ONGOING/TERMINATED_DRAW/TERMINATED_WINNER_A/TERMINATED_WINNER_B]

	Player playerA		//First  player
	Player playerB		//Second player
	
	Player active_player	//Player currently playing
	
	GameBoard board		//Board.
	
	bool turn_ended		//Not sure if Useful
	
*/////////////////////

/*///	 Methods	///

	void Start 		//Unused
	void Update		//Unused
	
	void check_state	//Check game_state, End game if needed, also destroy Units with 0 "hp"
	
	void play_game		//
	
	void start_game		//Not Yet Implemented
	void end_game		//Not Yet Implemented
	
	void play_turn		//Active player play his turn,Not Fully Implemented
	
	void start_turn
	void end_turn
	
	bool set_turn_ended(bool)	//Set turn_ended to inputted boolean,returns turn_ended
									//Use to trigger end of turn

	bool turn_not_ended			//returns not turn_ended
	
	void swap_active_player		//Change the Active Player
	Player get_active_player	//returns the Active Player
	

*///////////////////////


/*///	TODO	///

start_game	-> Not Yet Implemented
end_game	-> Not Yet Implemented

play_turn -> WIP


*/////////////////////
public class Game : MonoBehaviour
{
	public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	
	public enum G_state : byte //255 max possible states
	{
		NOT_STARTED,
		ONGOING,
		TERMINATED_DRAW,
		TERMINATED_WINNER_A,
		TERMINATED_WINNER_B
	}
	
	public G_state game_state;
	
	public Player playerA;	
	public Player playerB;	
	public Player active_player;
	
	public GameBoard board;	
	
	public bool turn_ended;
	
    void Start(){	}
    void Update(){   }
	

//Checking

	private void check_state(){
		if(game_state == G_state.TERMINATED_DRAW || game_state == G_state.TERMINATED_WINNER_A || game_state == G_state.TERMINATED_WINNER_B){
			if(DEBUG_PRINT){Debug.Log("["+this+"check_state()] Game Terminated");}
			end_game();
		}
		
		board.check_state();
	}
	
//Game management

	public void start_game()
	{
		
	}
	
	public void play_game(){
		while (game_state == G_state.ONGOING){
			play_turn();
		}
	}
	
	public void end_game()
	{	
		
	}
	
//Turn management
	
	public void play_turn()
	{	
		start_turn();
		
		while(turn_not_ended())
		{
			active_player.do_action();
			
			check_state();
		}
		
		end_turn();
	}
	
	private void start_turn()//Routine de debut de tour
	{
		set_turn_ended(false);
		swap_active_player();
		
		if(DEBUG_PRINT){Debug.Log("["+this+".start_turn()] "+active_player+"Turn started");}
		
		board.start_turn();//PLAYER IDENTIFIER ?
		active_player.start_turn();
		check_state();		
	}
	
	private void end_turn()	//Routine de fin de tour
	{
		if(DEBUG_PRINT){Debug.Log("["+this+".end_turn()] "+active_player+"Turn ended");}
		
		active_player.end_turn();
		board.end_turn();//PLAYER IDENTIFIER ?
		check_state();
	}
	
	
//turn_ended boolean management

	public bool set_turn_ended(bool t){
		if(SUSPICIOUS_WARNING){
			if(t==turn_ended){
				Debug.Log("WARNING : ["+this+".turn_ended("+t+")]  turn_ended already equal to inputted bool");
			}
		}
		turn_ended=t;
		return turn_ended;
	}
	
	public bool turn_not_ended(){
		return !turn_ended;
	}
	
//Active Player management
	
	public void swap_active_player(){
		
		if(active_player != playerA && active_player != playerB ){
			Debug.Log("ERROR : ["+this+".swap_active_player()] Invalid Active player "+active_player);
		
		}else{
			if(active_player == playerA){
				if(DEBUG_PRINT){Debug.Log("["+this+".swap_active_player()] "+active_player+"replaced by"+playerB);}
				active_player=playerB;
			}else{	//Do I really need to recheck if active_player == PlayerB ?
				if(DEBUG_PRINT){Debug.Log("["+this+".swap_active_player()] "+active_player+"replaced by"+playerA);}
				active_player=playerA;
			}
			
		}
		
		//Switch, Cleaner but doesn't work ^^
		/*
		switch(active_player)
		{
		case playerA:
			if(DEBUG_PRINT){Debug.Log("["+this+".swap_active_player()] "+active_player+"replaced by"+playerB);}
			active_player=playerB;
			break;
			
		case playerB:
			if(DEBUG_PRINT){Debug.Log("["+this+".swap_active_player()] "+active_player+"replaced by"+playerA);}
			active_player=playerA;
			break;
		
		default:
			Debug.Log("ERROR : ["+this+".swap_active_player()] Invalid Active player "+active_player);
			break;
		}
		*/
	}
	
	public Player get_active_player(){
		return active_player;
	}
	
}
