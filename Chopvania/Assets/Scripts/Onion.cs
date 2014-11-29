using UnityEngine;
using System.Collections;

public class Onion : MonoBehaviour {

	float runSpeed = 5f;
	public GameObject layer;
	public GameObject onionParticles;
	bool isFacingRight = false;
	float shootingTime = 2.0f;
	float reverseTime = 4f;
	int health = 3;
	
	void Start () {
		rigidbody2D.velocity = new Vector2(0.5f * runSpeed, 0f);
		Flip ();
	}

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
			reverseTime = 4f;
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
			Vector3 bulletPosition = transform.position;
			Vector3 offset = new Vector3(Orient(0.75f), 0.05f, -2f);
			bulletPosition += offset;
			GameObject bulletObject;
			if(!isFacingRight)
				 bulletObject = (GameObject)Instantiate(layer, bulletPosition, Quaternion.identity);
			else
				bulletObject = (GameObject)Instantiate(layer, bulletPosition, Quaternion.Euler (0f, 180f, 0f));
			float bulletSpeed = Orient(400f);
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
		if (collisionObject.tag == "PlayerWeapon")
		{
			Instantiate(onionParticles, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
			health--;
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			Instantiate(onionParticles, new Vector3(transform.position.x, transform.position.y, -5f), Quaternion.identity);
			health--;
		}
		if(health == 0){
			Destroy(gameObject);
		}
	}
}
