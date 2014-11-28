using UnityEngine;
using System.Collections;

public class Fallings : MonoBehaviour {

	GameObject Player;
	bool isNearPlayer = false;
	
	
	void Awake()
	{
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
