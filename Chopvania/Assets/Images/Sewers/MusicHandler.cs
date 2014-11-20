using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {

	public AudioSource bigSplash;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Water"){
			bigSplash.Play();
		}
	}
}
