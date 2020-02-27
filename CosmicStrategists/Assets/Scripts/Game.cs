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
	
	void check_state	//Check game_state, End game if needed, >also destroy Units with 0 "hp" Not Yet Implemented<
	
	void start_game		//Not Yet Implemented
	void end_game		//Not Yet Implemented
	
	void play_turn		//Active player play his turn,Not Fully Implemented
	
	void start_turn
	void end_turn
	

*///////////////////////


/*///	TODO	///

check_state -> destroy unit with no "hp"

start_game	-> Not Yet Implemented
end_game	-> Not Yet Implemented


play_turn -> WIP

Methode Pour changer le joueur actif entre les tours 


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
	
    void Start(){	}
    void Update(){   }
	
	
	void check_state(){
		if(game_state == G_state.TERMINATED_DRAW || game_state == G_state.TERMINATED_WINNER_A || game_state == G_state.TERMINATED_WINNER_B){
			end_game();
		}
		
		//Destroy already dead Unit TODO
	}
	
	
	void start_game()
	{
		
	}
	
	void end_game()
	{	
		
	}
	
	
	void play_turn()
	{	
		start_turn();
		
		//while(????) //La fin du tour
		{
			active_player.do_action();
			
			check_state();
		}
		
		end_turn();
	}
	
	void start_turn()//Routine de debut de tour
	{
		board.start_turn();//PLAYER IDENTIFIER ?
		active_player.start_turn();

		check_state();		
	}
	
	void end_turn()	//Routine de fin de tour
	{
		active_player.end_turn();
		board.end_turn();//PLAYER IDENTIFIER ?
		
		check_state();
	}
}
