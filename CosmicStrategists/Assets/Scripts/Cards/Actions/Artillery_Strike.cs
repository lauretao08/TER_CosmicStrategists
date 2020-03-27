using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery_Strike : Card_Action
{
	
    protected override void Activate()
    {
		get_opponent().lose_hp(2);
    }

}
