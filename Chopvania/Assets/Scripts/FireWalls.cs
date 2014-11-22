using UnityEngine;
using System.Collections;

public class FireWalls : MonoBehaviour {

	void Awake () {
		rigidbody2D.velocity = new Vector2 (0,0.7f);
	}

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

