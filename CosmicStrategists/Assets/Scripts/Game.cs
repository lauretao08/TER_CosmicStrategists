using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	
	public Player playerA;	//Premier joueur
	public Player playerB;	// Second joueur
	
	public Player activePlayer;	//Le joueur dont le tour est en cours
								//Pointeur ?
								
	public GameBoard board;	//Plateau de jeu
	
	
	
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }
	
	
	void checkState()
	{	//Verifie l'etat de la partie
		//(Si la partie est finie, si des unités sont mortes, etc)
		
	}
	
	
	void startGame()
	{	//Declenche le debut d'une partie
		//(Prepare les deux joueurs, le board)
		
		
		
	}
	
	void endGame()
	{	
		
	}
	
	
	void playTurn()
	{	
		startTurn();
		
		while(????) //La fin du tour
		{
			activePlayer.doAction();
			
			checkState();
		}
		
		endTurn();
	}
	
	void startTurn()//Routine de debut de tour
	{
		board.startTurn();//PLAYER IDENTIFIER ?
		activePlayer.startTurn();

		checkState();		
	}
	
	void endTurn()	//Routine de fin de tour
	{
		activePlayer.endTurn();
		board.endTurn()//PLAYER IDENTIFIER ?
		
		checkState();
	}
}
