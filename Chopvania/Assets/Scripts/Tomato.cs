using UnityEngine;
using System.Collections;

public class Tomato : MonoBehaviour{

	public GameObject explosion;
	public float speed = 3f;
	bool isNearPlayer;
	GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon" || collisionObject.tag == "Explosion"){
			Explode ();
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			Explode();
		}
	}

	public void Explode(){
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void Update(){
		isNearPlayer = Mathf.Abs(Vector2.Distance (transform.position, player.transform.position)) < 8f;
		if (isNearPlayer) {
			Vector3 direction = player.transform.position - transform.position;
			rigidbody2D.AddForce (direction.normalized * speed);
			transform.localRotation *= Quaternion.Euler (0f, 0f, 10f);
		}
		if ((Mathf.Abs (transform.position.x - player.transform.position.x)) < 1.5f && (Mathf.Abs (transform.position.y - player.transform.position.y)) < 2f) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}