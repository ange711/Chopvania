using UnityEngine;
using System.Collections;

public class FirwWalls : MonoBehaviour {

	Transform playerTransform;

	// Use this for initialization
	void Awake () {
	
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {


		if (rigidbody2D.velocity.y == 0) {

			rigidbody2D.velocity = new Vector2 (0,0.7f);

				}
	
	}
}
