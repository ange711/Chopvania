using UnityEngine;
using System.Collections;

public class BossCameraTrigger : MonoBehaviour {
	
	GameObject cam;
	
	void Awake(){
		cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player") {
			cam.GetComponent<BossCamera> ().zoomedOut = true;
		}
	}
}
