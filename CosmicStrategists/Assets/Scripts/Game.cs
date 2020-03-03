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
	
	public enum T_state : byte //Turn state
	{
		STARTING,
		ACTIVE,
		ENDING
	}
	
	public T_state turn_state;
	
	public Player playerA;	
	public Player playerB;	
	private Player active_player;
	
	public GameBoard board;	
	
	
    void Start(){	
		start_game();
	}

    void Update(){ 
		play_game();
	}
	

//Checking

	private void check_state(){
		if(game_state == G_state.TERMINATED_DRAW || game_state == G_state.TERMINATED_WINNER_A || game_state == G_state.TERMINATED_WINNER_B){
			if(DEBUG_PRINT){Debug.Log("["+this+"check_state()] Game Terminated");}
			end_game();
		}
		
		//board.check_state();
	}
	
//Game management

	public void start_game()
	{
		game_state=G_state.NOT_STARTED;
		 
		playerA.init();
		playerB.init();
	
		active_player=PlayerB;
		game_state=G_state.ONGOING;
	}
	
	public void play_game(){
		if(game_state==G_state.ONGOING){
			play_turn();
		}
	}
	
	public void end_game()
	{	
		
	}
	
//Turn management
	
	public void play_turn()
	{	
		switch(turn_state){
		case T_state.STARTING:
			start_turn();
			break;
		case T_state.ACTIVE:
			
			break;
		case T_state.ENDING:		
			end_turn();
			break;
		
		}
		
	}
	
	private void start_turn()//Routine de debut de tour
	{
		swap_active_player();
		
		if(DEBUG_PRINT){Debug.Log("["+this+".start_turn()] "+active_player+"Turn started");}
		
		//board.start_turn();//PLAYER IDENTIFIER ?
		active_player.start_turn();
		check_state();	
		turn_state=T_state.ACTIVE;
	}
	
	private void end_turn()	//Routine de fin de tour
	{
		
		if(DEBUG_PRINT){Debug.Log("["+this+".end_turn()] "+active_player+"Turn ended");}
		
		active_player.end_turn();
		//board.end_turn();//PLAYER IDENTIFIER ?
		check_state();
		
		turn_state=T_state.STARTING;
	}
	

	public void finish_turn(){
		if(DEBUG_PRINT){Debug.Log("["+this+".finish_turn()] Finishing turn");}
		
		turn_state=T_state.ENDING;
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
