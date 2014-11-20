using UnityEngine;
using System.Collections;

public class MiniShroom : MonoBehaviour {

	public LayerMask groundMask;
	float speed = 3f;
	GameObject player;
	CircleCollider2D circlecollider;
		
	void Awake()
	{
		circlecollider = GetComponent<CircleCollider2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		Destroy (gameObject, 12f);
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
		rigidbody2D.velocity = new Vector2 (-speed, 0f);
	}
}
