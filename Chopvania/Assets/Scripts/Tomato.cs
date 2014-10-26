using UnityEngine;
using System.Collections;

public class Tomato : MonoBehaviour{

	public GameObject explosion;
	float speed = 3f;
	bool isNearPlayer;
	GameObject player;
	BoxCollider2D boxcollider;

	
	void Awake()
	{
		boxcollider = GetComponent<BoxCollider2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	void Update(){
		isNearPlayer = Mathf.Abs(transform.position.x - player.transform.position.x) < 8f;
		if (isNearPlayer) {
			var direction = player.transform.position - transform.position;
			rigidbody2D.AddForce (direction.normalized * speed);
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) < (boxcollider.size.x/1.5f)) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}