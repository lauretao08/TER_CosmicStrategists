using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*///	Attributes  ///
	
	bool DEBUG_PRINT		//enable/disable logs in debug console
	bool SUSPICIOUS_WARNING	//enable/disable logs for strange behaviors

	List<Unit> playerA_units	//List of all player A's Units
	List<Unit> playerB_units //List of all player B's Units
	
*/////////////////////


/*///	 Methods	///

	Start		//Unused
	Update		//Unused

	start_turn	//NYI, Recharges all units controlled by the active player
	end_turn	//NYI

	check_state		//NYI, check if units must be destroyed

*//////////////////////

/*///	TODO	///

	Units !!
	check_state
	
	start_turn


*//////////////////

public class GameBoard : MonoBehaviour
{
	
	public List<Unit> playerA_units;
	public List<Unit> playerB_units;
    public List<GameObject> playerA_units_go;
    public List<GameObject> playerB_units_go;

    public List<Structure> playerA_structures;
    public List<Structure> playerB_structures;
    public List<GameObject> playerA_structures_go;
    public List<GameObject> playerB_structures_go;

    public GameObject Board;

    void Start(){	}
	void Update(){	}
	
	public void start_turn()
	{
		
	}
	
	public void end_turn()
	{
		
	}
	
	public void check_state(){
		
	}


    //**UNITS**//
    public void add_unit_A(Unit unit)
    {
        playerA_units.Add(unit);
    }

    public void add_unit_B(Unit unit)
    {
        playerB_units.Add(unit);
    }

    public void add_unit_A_go(GameObject unit)
    {
        playerA_units_go.Add(unit);
        Unit tmp = unit.GetComponent(typeof(Unit)) as Unit;
        add_unit_A(tmp);
        PlaceUnitsA();
    }

    public void add_unit_B_go(GameObject unit)
    {
        playerB_units_go.Add(unit);
        Unit tmp = unit.GetComponent(typeof(Unit)) as Unit;
        add_unit_B(tmp);
        PlaceUnitsB();
    }

    //**STRUCTURES**//
    public void add_structure_A(Structure structure)
    {
        playerA_structures.Add(structure);
    }

    public void add_structure_B(Structure structure)
    {
        playerB_structures.Add(structure);
    }

    public void add_structure_A_go(GameObject structure)
    {
        playerA_structures_go.Add(structure);
        Structure tmp = structure.GetComponent(typeof(Structure)) as Structure;
        add_structure_A(tmp);
        PlaceStructuresA();
    }

    public void add_structure_B_go(GameObject structure)
    {
        playerB_structures_go.Add(structure);
        Structure tmp = structure.GetComponent(typeof(Structure)) as Structure;
        add_structure_B(tmp);
        PlaceStructuresB();
    }

    //**UNITS**//

    void PlaceUnitsA()
    {
        Vector3 base_pos = Board.transform.position;
        base_pos.z -= 15.0f;
        base_pos.y += 1.0f;
        base_pos.x -= 25.0f;

        
        foreach (GameObject c in playerA_units_go)
        {
            c.transform.position = base_pos;
            
            base_pos.x += 3.0f;
        }
    }

    void PlaceUnitsB()
    {
        Vector3 base_pos = Board.transform.position;
        base_pos.z += 15.0f;
        base_pos.y += 1.0f;
        base_pos.x += 25.0f;


        foreach (GameObject c in playerB_units_go)
        {
            c.transform.position = base_pos;
            
            base_pos.x -= 3.0f;
        }
    }


    //**STRUCTURES**//
    void PlaceStructuresA()
    {
        Vector3 base_pos = Board.transform.position;
        base_pos.z -= 25.0f;
        base_pos.y += 1.0f;
        base_pos.x += 25.0f;


        foreach (GameObject c in playerA_structures_go)
        {
            c.transform.position = base_pos;

            base_pos.x -= 3.0f;
        }
    }

    void PlaceStructuresB()
    {
        Vector3 base_pos = Board.transform.position;
        base_pos.z += 15.0f;
        base_pos.y += 1.0f;
        base_pos.x -= 25.0f;


        foreach (GameObject c in playerB_structures_go)
        {
            c.transform.position = base_pos;

            base_pos.x += 3.0f;
        }
    }

}
