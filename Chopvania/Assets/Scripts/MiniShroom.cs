using UnityEngine;
using System.Collections;

public class MiniShroom : MonoBehaviour {



	GameObject boss;
	bool canTurn = true;
	bool canJump = true;
	int turnValue = -1;


	void Awake()
	{
		rigidbody2D.velocity = new Vector3 (5f * turnValue, 0f, 0f);
		boss = GameObject.FindGameObjectWithTag ("Boss");
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
			transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
			canTurn = false;
			Invoke ("Turner", 1);
			turnValue *= -1;
			collisionObject.SendMessage ("ApplyDamage", 1);
		}


		if((collisionObject.tag == "Boss" || collisionObject.tag == "Wall" || collisionObject.tag == "Enemy") && canTurn){
			transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
			canTurn = false;
			Invoke ("Turner", 1);
			turnValue *= -1;
		}
	}
	
	void Update(){
		rigidbody2D.velocity = new Vector3 (5f * turnValue, 0f, 0f);
		if (canJump) {
			canJump = false;
			rigidbody2D.AddForce (new Vector3(0f, 5000f, 0f));
			Invoke("Jumper", 3);
		}
    }
	
	void Turner(){
		canTurn = true;
	}

	void Jumper(){
		canJump = true;
	}


}
