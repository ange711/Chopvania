using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour
{

	int damage = 1;
	bool isActive = true;
	CircleCollider2D circlecollider;

	
	
	void Awake()
	{
		circlecollider = GetComponent<CircleCollider2D>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero> ().IsInvincible () && isActive) {
			collisionObject.SendMessage ("ApplyDamage", damage);
		}
		
		if(collisionObject.tag == "PlayerWeapon"){
			circlecollider.isTrigger = false;
			rigidbody2D.gravityScale = 10f;
			isActive = false;
		}
	}

}