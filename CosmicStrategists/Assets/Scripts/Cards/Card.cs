using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Parameters
    public string card_name="-"; //name of the card
    public int card_base_energy_cost=0; //base energy cost of the card
    private int card_energy_cost = 0; //actual energy cost of the card
    [TextArea]
    public string card_description="-"; // displayed description on the card



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnPlay is called when the card is played
    void OnPlay()
    {

    }
}
