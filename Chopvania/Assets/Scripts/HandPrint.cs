using UnityEngine;
using System.Collections;

public class HandPrint : MonoBehaviour{
	
	public GameObject bar;
	public GameObject knife;
	public GameObject skillet;
	public int weaponSpawned = 1;
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			createWeapon();
			Destroy (gameObject);
		}
	}

	void createWeapon(){
		Vector3 Offset = new Vector3(0f, -0.7f, 0f);
		switch(weaponSpawned){
		case 1:
			GameObject Bar = (GameObject)Instantiate(bar, transform.position + Offset, Quaternion.identity);
			break;
			
		case 2:
			GameObject Knife = (GameObject)Instantiate(knife, transform.position + Offset, Quaternion.identity);
			break;
			
		case 3:
			GameObject Skillet = (GameObject)Instantiate(skillet, transform.position + Offset, Quaternion.identity);
			break;
		}
	}

}