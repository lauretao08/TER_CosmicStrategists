using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Hunter : Unit
{
    public override void start_turn_active()
    {
        
    }

    public override void on_arrival_active()
    {

    }

    public override void end_turn_active()
    {
        
    }

    public override void ActivateEffect()
    {
        Unit target = active_effect_target.GetComponent(typeof(Unit)) as Unit;
        target.damage(2);
    }

}
