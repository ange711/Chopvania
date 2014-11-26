using UnityEngine;
using System.Collections;

public class Onion : MonoBehaviour {

	public float runSpeed = 5f;
	public GameObject bullet1;
	public GameObject bullet2;
	bool isFacingRight = false;
	Animator animator;
	PolygonCollider2D polyCollider;
	float shootingTime = 2.0f;
	float reverseTime = 2.5f;
	public GameObject explosion;
	int health = 3;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		polyCollider = GetComponent<PolygonCollider2D>();
		rigidbody2D.velocity = new Vector2(0.5f * runSpeed, 0f);
		Flip ();
	}
	
	// Update is called once per frame
	void Update () {
		bool reverse = updateReverse ();
		if (reverse)
		{
			Flip ();
			rigidbody2D.velocity = -rigidbody2D.velocity;
		}
		UpdateShooting();
	}

	bool updateReverse()
	{
		reverseTime = Mathf.Max(0, reverseTime - Time.deltaTime);
		if (reverseTime <= 0) {
			reverseTime = 2.5f;
			return true;

		} else
			return false;
	}

	float Orient(float value)
	{
		return isFacingRight ? value : -value;
	}

	void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 nextScale = transform.localScale;
		nextScale.x *= -1f;
		transform.localScale = nextScale;
	}

	void UpdateShooting()
	{
		shootingTime = Mathf.Max(0, shootingTime - Time.deltaTime);
		if (shootingTime <= 0)
		{
			shootingTime = 2.0f;
			Vector2 bulletPosition = transform.position;
			Vector2 offset = new Vector2(Orient(0.75f), 0.05f);
			bulletPosition += offset;
			GameObject bulletObject;
			if(isFacingRight)
				 bulletObject = (GameObject)Instantiate(bullet1, bulletPosition, Quaternion.identity);
			else
				bulletObject = (GameObject)Instantiate(bullet2, bulletPosition, Quaternion.identity);
			float bulletSpeed = Orient(500f);
			bulletObject.rigidbody2D.AddForce(new Vector2(bulletSpeed, 0));
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("ApplyDamage", 1);
		}
		else if (collisionObject.tag == "PlayerWeapon")
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			--health;
			if(health == 0){
				Destroy(gameObject);
			}
		}
	}
}
