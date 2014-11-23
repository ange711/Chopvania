using UnityEngine;
using System.Collections;

public class FireWalls : MonoBehaviour {

	void Update () {
		rigidbody2D.velocity = new Vector2 (0,0.7f);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player") {
			collisionObject.SendMessage ("ApplyDamage", 99999);
		} 
		if(collisionObject.tag != "Fire" && collisionObject.tag != "Player"){
			Destroy(collisionObject.gameObject);
		}
	}
	
}

