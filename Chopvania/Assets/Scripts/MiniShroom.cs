using UnityEngine;
using System.Collections;

public class MiniShroom : MonoBehaviour {
	
	GameObject player;
	GameObject boss;
	CircleCollider2D circlecollider;
	bool jumpCD = true;
		
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		boss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			boss.GetComponent<Boss>().MinionMinus();
			Destroy(gameObject);
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			Destroy (gameObject);
		}
		if (collisionObject.tag == "Player") 
		{
			collisionObject.SendMessage ("ApplyDamage", 1);
		}
	}
	
	void Update(){
		if (jumpCD && (Vector2.Distance (player.transform.position, transform.position) < 0f)) {
			rigidbody2D.AddForce(new Vector2(1, 20f));
			jumpCD = false;
		}
		if (jumpCD && (Vector2.Distance (player.transform.position, transform.position) > 0f)) {
			rigidbody2D.AddForce(new Vector2(-1, 20f));
			jumpCD = false;
		}
		if (!jumpCD)
			Invoke ("JumpReset", 2f);

	}

	void JumpReset(){
		jumpCD = true;
	}
}
