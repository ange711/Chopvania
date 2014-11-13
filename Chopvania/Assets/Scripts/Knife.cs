using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{
	public bool isOnFloor = false;
	public int Ammo = 1;


	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor && Input.GetButtonDown("Fire2")){
			collisionObject.SendMessage ("pickUpKnife", Ammo);
			Destroy(gameObject);
		}
		if (collisionObject.tag != "Player" && collisionObject.tag != "Ladder" && !isOnFloor){
			Destroy(gameObject);
		}
	}

	public void setAmmo(int ammo){
		Ammo = ammo;
	}
	
}