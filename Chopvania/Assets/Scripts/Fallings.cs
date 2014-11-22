using UnityEngine;
using System.Collections;

public class Fallings : MonoBehaviour {
	
	Transform playerTransform;
	GameObject Player;
	bool isNearPlayer = false;
	
	
	void Awake()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update()
	{
		isNearPlayer = Vector2.Distance (transform.position, Player.transform.position) < 4f;
		if(isNearPlayer)
			rigidbody2D.velocity = new Vector2 (0, -3);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player") {
			collisionObject.SendMessage ("ApplyDamage", 1);
			Destroy(gameObject);
		} 
		else {
			Destroy(gameObject);
		}
		
	}
}
