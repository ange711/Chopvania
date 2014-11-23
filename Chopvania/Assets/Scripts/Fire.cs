using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player") {
			collisionObject.SendMessage ("ApplyDamage", 99999);
		} 
		else {
			Destroy(collisionObject.gameObject);
		}
	}
	
}



