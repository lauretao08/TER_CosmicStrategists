using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_OutlawDriver : Unit
{
    private Unit buffed_unit;
    public int buff_amount = 2;

    public override void start_turn_active()
    {
        //debuff unit buffed last turn on turn start
        if (buffed_unit != null)
        {
            buffed_unit.change_damage(buffed_unit.get_damage() - buff_amount);
            buffed_unit = null;
        }
        
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
        buffed_unit = target;
        target.change_damage(target.get_damage() + buff_amount);

    }


}
