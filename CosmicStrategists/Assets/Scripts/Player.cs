using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	public int manaMax;
	public int mana;
	
	/*
	private List<Card>  deck;
	private List<Card>  graveyard;
	private List<Card>	hand;
	*/
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {    
    }
	
	void init()
	{
		manaMax=0;
		mana=0;
	
		/*
		deck=new List<Card>;
		hand=new List<Card>;
		graveyard=new List<Card>;
		*/
		
	}
	
	void startTurn()
	{
		manaMax++;
		mana=manaMax;
		
		drawCard();
	}
	
	void endTurn()
	{
		
	}
	
	
}
