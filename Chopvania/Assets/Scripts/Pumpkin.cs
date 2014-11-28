using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour
{

	int damage = 1;
	int health = 2;
	bool isActive = true;
	CircleCollider2D circlecollider;
	public GameObject pumpkinParticle;
	
	
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
			health--;
			Instantiate(pumpkinParticle, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			health--;
			Instantiate(pumpkinParticle, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
		}
		if(health == 0){
			Instantiate(pumpkinParticle, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
			circlecollider.isTrigger = false;
			rigidbody2D.gravityScale = 10f;
			isActive = false;
			GetComponent<Animator>().SetBool("isDead", true);
		}
	}

}