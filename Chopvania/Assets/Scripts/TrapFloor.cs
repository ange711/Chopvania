using UnityEngine;
using System.Collections;

public class TrapFloor : MonoBehaviour {

	float fallTimer = 0.3f;
	bool timerOn = false;


	void Update(){
		if(timerOn){
			fallTimer -= Time.deltaTime;
		}

		if(fallTimer <= 0f){
			GetComponent<Rigidbody2D>().gravityScale = 40;
		}

	}


	void OnTriggerStay2D(Collider2D col){

		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player"){
			timerOn = true;
		}
	}
}
