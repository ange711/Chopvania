using UnityEngine;
using System.Collections;

public class Tomato : MonoBehaviour
{
	public GameObject explosion;
	public float speed = 2f;
	bool isNearPlayer;
	float activeTime;
	//Animator animator;
	GameObject player;

	
	void Awake()
	{
		//animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		activeTime = 0.19f;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("ApplyDamage", 10);
		}
		else if (collisionObject.tag == "PlayerBullet")
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	void Update(){
		isNearPlayer = Mathf.Abs(transform.position.x - player.transform.position.x) < 8f;
		if (isNearPlayer) {
			var direction = player.transform.position - transform.position; //vector from enemy to player
			rigidbody2D.AddForce(direction.normalized * speed);//normalized direction so it doesnt fly super fast
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) < 1.15f) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}