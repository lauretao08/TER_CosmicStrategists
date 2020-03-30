using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DeckJson
{
    public List<int> Deck;

    public DeckJson()
    {
        Deck = new List<int>();
    }
}


public class DeckLoader
{
	public enum Card_ID{
		SCOUT,
		ARTILLERY_STRIKE,
		COMMUNICATIONS_BASE,
		COMMON_FRIGATE,
		HYMPERIUM_CRUISER
    }

    public DeckLoader()
    {

    }


    // Loads a deck from data file
    public List<int> LoadFromFileInId(string fileName)
    {
        StreamReader r = new StreamReader(fileName);
        string json = r.ReadToEnd();

        DeckJson card_ids = new DeckJson();

        card_ids = JsonUtility.FromJson<DeckJson>(json);
        
        List<int> deck = new List<int>();

        if (card_ids.Deck != null)
        {
            deck = card_ids.Deck;
        }
        else
        {
            Debug.Log("DECKJSON IS NULL ! ");
        }

        return deck;
    }

    //return a card prefab for instanciation
    public GameObject GenerateCardFromId(int ID)
    {
		/*
        switch (ID)
        {
            case 1:
                return Resources.Load("Card/Units/Scout") as GameObject;
            case 2:
                return Resources.Load("Card/Actions/Artillery_Strike") as GameObject;
            case 3:
                return Resources.Load("Card/Structures/Communications_base") as GameObject;
            default:
                return Resources.Load("Card/Units/Scout") as GameObject;
        }
		*/
		 
        switch ((Card_ID) ID)
        {
            case Card_ID.SCOUT:
                return Resources.Load("Card/Units/Scout") as GameObject;
            case Card_ID.ARTILLERY_STRIKE:
                return Resources.Load("Card/Actions/Artillery_Strike") as GameObject;
            case Card_ID.COMMUNICATIONS_BASE:
                return Resources.Load("Card/Structures/Communications_base") as GameObject;
			case Card_ID.COMMON_FRIGATE:
                return Resources.Load("Card/Units/CommonFrigate") as GameObject;
            case Card_ID.HYMPERIUM_CRUISER:
                return Resources.Load("Card/Units/HymperiumCruiser") as GameObject;
            	
				
            default:
				Debug.Log("Error while loading card with id"+ID+", replaced by scout");
                return Resources.Load("Card/Units/Scout") as GameObject;
        }
    }

}
