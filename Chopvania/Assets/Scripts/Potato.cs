using UnityEngine;
using System.Collections;

public class Potato : MonoBehaviour {

	public int health = 8;
	public GameObject explosion;
	GameObject player;
	BoxCollider2D boxcollider;
	bool idle = true;
	float speed = 4f;
	int damage = 4;


	void Awake(){
		boxcollider = GetComponent<BoxCollider2D>();
		player = GameObject.FindGameObjectWithTag("Player");

	}

	void Update () {
		if(idle && Mathf.Abs(player.transform.position.x - transform.position.x) < 15f){
			idle = false;
			GetComponent<Animator>().SetBool("isAngry", true);
			gameObject.AddComponent<Rigidbody2D>();
		}

		if(!idle){
			var direction = player.transform.position - transform.position;
			rigidbody2D.AddForce (direction.normalized * speed);
		}

	}

	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			--health;
			if(health == 0){
				Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}

		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
		}
	}
}
