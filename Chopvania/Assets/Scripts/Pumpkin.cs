using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour
{
	float speed = 3f;
	bool isNearPlayer;
	CircleCollider2D circlecollider;

	
	
	void Awake()
	{
		circlecollider = GetComponent<CircleCollider2D>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			circlecollider.isTrigger = false;
			rigidbody2D.gravityScale = 1f;
		}
	}
	
	void Update(){
	}
}