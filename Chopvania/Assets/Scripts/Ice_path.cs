using UnityEngine;
using System.Collections;

public class Ice_path : MonoBehaviour {
	
	Transform playerTransform;
	GameObject Player;
	bool isFacingRight = false;
	bool onTheIce =false;
	bool notInAir =false;
	// Use this for initialization
	void Awake()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update()
	{
		onTheIce = Mathf.Abs (transform.position.x - Player.transform.position.x) < 6.8f;
		notInAir = Mathf.Abs (transform.position.y - Player.transform.position.y) < 2f;
		
		
		
		if (onTheIce && notInAir) {
			
			isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().isFacingRight;
			if(!isFacingRight)
			{
				Vector2 force = new Vector2(-700f,0);
				Player.rigidbody2D.AddForce(force);
			}
			else
			{
				Vector2 force = new Vector2(700f,0);
				Player.rigidbody2D.AddForce(force);
			}
			
		}
		
		
	}
}