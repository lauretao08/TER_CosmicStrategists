using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
	public bool DEBUG_PRINT;
	public bool SUSPICIOUS_WARNING;
	public byte STARTING_HAND_SIZE;
	
	public enum G_state : byte //255 max possible states
	{
		NOT_STARTED,
		ONGOING,
		TERMINATED_DRAW,
		TERMINATED_WINNER_A,
		TERMINATED_WINNER_B
	}
	
	
	public enum T_state : byte //Turn state
	{
		STARTING,
		ACTIVE_A,
		ACTIVE_B,
		ENDING,
		OUT_OF_TURN
	}
	
	private G_state game_state;
	private T_state turn_state;
	
	public Player playerA;	
	public Player playerB;	
	private Player active_player;
	private Player inactive_player;
	
	public GameBoard board;	
	
    public Canvas card_ui;
    private CardFeedbackUI card_feedback_controller;
	
	public Canvas canvas_pause;
	private PauseMenu pause_menu;

    public Canvas canvas_unit_feedback;
    private UnitFeedBackUI unit_feedback_ui;


	private bool alreadyEnding = false;
	public bool paused;
	
    void Start(){	
        card_feedback_controller = card_ui.GetComponent(typeof(CardFeedbackUI)) as CardFeedbackUI;
		
		pause_menu = canvas_pause.GetComponent(typeof(PauseMenu)) as PauseMenu;
        unit_feedback_ui = canvas_unit_feedback.GetComponent(typeof(UnitFeedBackUI)) as UnitFeedBackUI;

    }

    void Update(){ 
		paused = pause_menu.GameIsPaused;
		switch (game_state){
		case G_state.NOT_STARTED:
			start_game();
			break;
			
		case G_state.ONGOING:
			play_game();
			break;
			
		case G_state.TERMINATED_DRAW:
		case G_state.TERMINATED_WINNER_A:
		case G_state.TERMINATED_WINNER_B:
			end_game();		
			break;

		default:
			Debug.Log("ERROR : Invalid game_state !");
			break;
		}
	}
	

//Checking

	private void check_state(){
		if(playerA.is_dead()&&playerB.is_dead()){
			game_state = G_state.TERMINATED_DRAW;
		}else{
			if(playerA.is_dead()){
				game_state = G_state.TERMINATED_WINNER_B;
			}
			if(playerB.is_dead()){
				game_state = G_state.TERMINATED_WINNER_A;
			}
		}
		
		board.check_state();
	}
	
//Game management

	public void start_game()
	{
		playerA.init();
		playerB.init();
	
		playerA.get_card_controller().Draw(3);
		playerB.get_card_controller().Draw(4);
	
		active_player=playerB;
		
		game_state=G_state.ONGOING;
	}
	
	public void play_game(){
		if(game_state==G_state.ONGOING){
			play_turn();
		}
	}
	
	public void end_game()
	{	
		if(!alreadyEnding){
			alreadyEnding=true;
			//Display Winner
			switch (game_state){
				
			case G_state.TERMINATED_DRAW:
				card_feedback_controller.WriteEndGame("Egalité");
				break;
			case G_state.TERMINATED_WINNER_A:
				card_feedback_controller.WriteEndGame("Victoire");
				break;
			case G_state.TERMINATED_WINNER_B:	
				card_feedback_controller.WriteEndGame("Defaite");
				break;

			case G_state.NOT_STARTED:	
			case G_state.ONGOING:
			default:
				Debug.Log("ERROR : Invalid game_state !");
				break;
			}
			//Fade to black
			Initiate.Fade("MenuPrincipal",Color.black,2.0f);
		}
	}
	
//Turn management
	
	public void play_turn(){
	if(paused){
			return;
		}
		check_state();
		switch(turn_state){
		case T_state.STARTING:
			start_turn();
			break;
		
		case T_state.ACTIVE_A:
			if(active_player.is_robot()){active_player.auto_turn();}
			break;
			
		case T_state.ACTIVE_B:
			if(active_player.is_robot()){active_player.auto_turn();}
			break;
			
		case T_state.ENDING:		
			end_turn();
			break;
		
		case T_state.OUT_OF_TURN:
			break;
			
		default:
			Debug.Log("ERROR : Invalid turn_state !");
			break;
		}
		
	}
	
	private void start_turn()//Routine de debut de tour
	{
		swap_active_player();
		
		if(DEBUG_PRINT){Debug.Log("["+this+".start_turn()] "+active_player+"Turn started");}
		card_feedback_controller.WriteTurnDisplay("Tour de "+active_player.name);
		
		active_player.start_turn();
		board.start_turn();
		check_state();
		
		if(active_player==playerA){
			turn_state=T_state.ACTIVE_A;
		}else{
			turn_state=T_state.ACTIVE_B;
		}
	}
	
	private void end_turn()	//Routine de fin de tour
	{
		
		if(DEBUG_PRINT){Debug.Log("["+this+".end_turn()] "+active_player+"Turn ended");}
		
		board.end_turn();
		active_player.end_turn();
		check_state();
		
		turn_state=T_state.STARTING;
	}
	

	public void finish_turn(){
		if(turn_state!=T_state.ACTIVE_A && turn_state!=T_state.ACTIVE_B){
			if(SUSPICIOUS_WARNING){Debug.Log("WARNING : ["+this+".finish_turn()] Used while Turn not active");}
		}
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
				inactive_player=playerA;
			}else{	//Do I really need to recheck if active_player == PlayerB ?
				if(DEBUG_PRINT){Debug.Log("["+this+".swap_active_player()] "+active_player+"replaced by"+playerA);}
				active_player=playerA;
				inactive_player=playerB;
			}
			
		}
	}
	
	public Player get_active_player(){
		return active_player;
	}

	
	public Player get_inactive_player(){
		return inactive_player;
	}
	
	public bool get_id_active_player(){ //False = A // True = B
		if(active_player == playerA){
			return false;
		}else{
			return true;
		}
	}
	
//Display management
	public void display_feedback_card_played(Player card_owner,string card_name){
		if(active_player == playerB){
			card_feedback_controller.WritePlayerB("Card played : "+card_name);
		}
		if(active_player == playerA){
			card_feedback_controller.WritePlayerA("Card played : "+card_name);
		}
	}

    //Unit feedback management (here unit also encompasses structures)

    public void display_feedback_unit(Card card)
    {
        unit_feedback_ui.DisplayCardInfo(card);
    }

    public void activate_feedback_unit(bool on)
    {
        if (on)
        {
            unit_feedback_ui.gameObject.SetActive(true);
        }
        else
        {
            unit_feedback_ui.gameObject.SetActive(false);
        }
    }

}

