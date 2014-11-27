using UnityEngine;
using System.Collections;

public class Skillet : MonoBehaviour
{
	public bool isOnFloor = false;
	public int Ammo = 1;
	BoxCollider2D boxcollider;

	void Awake(){
		boxcollider = GetComponent<BoxCollider2D>();
		if (!isOnFloor) {
			Invoke ("Shield", 0.2f);
		}

	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor && Input.GetButtonDown("Fire2")){
			if(collisionObject.GetComponent<Hero>().getWeaponsCloseBy () == 1 && collisionObject.GetComponent<Hero>().getWeaponType () == 0){
				collisionObject.SendMessage ("pickUpSkillet", Ammo);
				collisionObject.SendMessage ("weaponOutOfRange");
				Destroy(gameObject);
			}
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

	void Shield(){
		boxcollider.isTrigger = false;
	}

	public bool getCollider(){
		return boxcollider.isTrigger;
	}

	
}