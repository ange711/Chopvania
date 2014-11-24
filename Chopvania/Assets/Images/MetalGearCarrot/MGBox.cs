using UnityEngine;
using System.Collections;

public class MGBox : MonoBehaviour {

	public GameObject destruction;
	public int life  = 2;

	void Update(){
		if(life <= 0){
			Instantiate (destruction, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			--life;
		}
	}
}
