using UnityEngine;

public class PlayCollision : MonoBehaviour
{
    
	void OnCollisionEnter(Collision collisionInfo){
		Debug.Log(collisionInfo.collider.name);
	}


}
