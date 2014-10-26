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
		isNearPlayer = Mathf.Abs (transform.position.x - Player.transform.position.x) < 1;
		if (isNearPlayer) 
		{
			float fallingTime = 1.0f;
			if (fallingTime > 0.1) {
				rigidbody2D.velocity = new Vector2 (0, -6);
				fallingTime -= Time.deltaTime;
				}
			else 
			{
				Destroy(gameObject);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("ApplyDamage", 1);
		}

	}
}
