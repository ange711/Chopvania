using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	public string levelName;
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player"){
			Application.LoadLevel(levelName);
		}
	}
}
