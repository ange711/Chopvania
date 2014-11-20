using UnityEngine;
using System.Collections;

public class Tentacle : MonoBehaviour {

	float speed = 2f;
	GameObject player;
	PolygonCollider2D polycollider;
	
	void Awake()
	{
		polycollider = GetComponent<PolygonCollider2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		Destroy (gameObject, 2f);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon" || collisionObject.tag == "Wall"){
			Destroy(gameObject);
		}
		if (collisionObject.tag == "Player") 
		{
			collisionObject.SendMessage ("ApplyDamage", 2);
		}
	}
	
	void Update(){
		rigidbody2D.velocity = new Vector2 (0f, -speed);
	}
}
