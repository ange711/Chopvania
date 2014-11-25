using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour
{
	public bool isOnFloor = false;
	public int Ammo = 1;
	
	
	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor && Input.GetButtonDown("Fire2")){
			if(collisionObject.GetComponent<Hero>().getWeaponsCloseBy () == 1 && collisionObject.GetComponent<Hero>().getWeaponType () == 0){
				collisionObject.SendMessage ("pickUpBar", Ammo);
				collisionObject.SendMessage ("weaponOutOfRange");
				Destroy(gameObject);
			}
		}
		if (collisionObject.tag != "Player" && collisionObject.tag != "Ladder" && !isOnFloor){
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor)
			collisionObject.SendMessage ("weaponInRange");
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor)
			collisionObject.SendMessage ("weaponOutOfRange");
	}
	
	public void setAmmo(int ammo){
		Ammo = ammo;
	}
	
}