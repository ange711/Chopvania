using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{
	public bool isOnFloor;
	
	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor && Input.GetButtonDown("Fire2")){
			collisionObject.SendMessage ("pickUpKnife");
			Destroy(gameObject);
		}
		if (collisionObject.tag != "Player" && collisionObject.tag != "Ladder" && !isOnFloor){
			Destroy(gameObject);
		}
		
	}
	
}