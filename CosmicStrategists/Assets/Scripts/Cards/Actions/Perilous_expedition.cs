using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perilous_expedition : Card_Action
{
    protected override void Activate()
    {
        int damage_taken = 0;
   

        //draw 3 cards, take 1 damage for each unit drawn
        Card c1 = get_owner().get_card_controller().Draw(1);
        Card c2 = get_owner().get_card_controller().Draw(1);
        Card c3 = get_owner().get_card_controller().Draw(1);
        if(c1 is Card_Unit)
        {
            damage_taken++;
        }
        if (c2 is Card_Unit)
        {
            damage_taken++;
        }
        if (c3 is Card_Unit)
        {
            damage_taken++;
        }
        if (damage_taken > 0)
        {
            get_owner().lose_hp(damage_taken);
        }
    }
}
