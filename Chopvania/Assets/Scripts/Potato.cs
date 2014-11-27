using UnityEngine;
using System.Collections;

public class Potato : MonoBehaviour {
	
	public int health = 8;
	public GameObject explosion;
	GameObject player;
	BoxCollider2D boxcollider;
	bool idle = true;
	float speed = 4f;
	int damage = 4;
	float jumpTimer = 0f;
	float invincibleTime;
	SpriteRenderer spriteRenderer;
	
	void Awake(){
		boxcollider = GetComponent<BoxCollider2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}
	
	void Update () {
		UpdateHit ();
		if(idle && Mathf.Abs(player.transform.position.x - transform.position.x) < 15f){
			idle = false;
			jumpTimer = 1.0f;
			GetComponent<Animator>().SetBool("isAngry", true);
			gameObject.AddComponent<Rigidbody2D>();
		}
		
		if(!idle){
			Vector2 direction = player.transform.position - transform.position;
			rigidbody2D.AddForce (new Vector2((direction.normalized).x * speed, 0f));
			jumpTimer -= Time.deltaTime;
			if(direction.x < 5f && jumpTimer < 0){
				rigidbody2D.AddForce(new Vector2(0, 100f));
				jumpTimer= 1.0f;
			}
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			--health;
			invincibleTime = 0.5f;
		}

		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			--health;
			invincibleTime = 0.5f;
		}

		if(health == 0){
			Destroy(gameObject);
			GameObject walls = GameObject.FindGameObjectWithTag("ExplodingWall");
			Vector2 wall = new Vector2(walls.transform.position.x, walls.transform.position.y);
			var explode = (GameObject) Instantiate(explosion, wall, Quaternion.identity);
			explode.transform.localScale = new Vector3(9.0f, 9.0f, 2.0f);
			walls.AddComponent("TimedDeath");
			walls.GetComponent<TimedDeath>().lifeTime = 1f;
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
			float blowback = Mathf.Sign(player.transform.position.x - transform.position.x);
			rigidbody2D.AddForce(new Vector2(blowback * -500f, 0));
		}
	}
	
	void UpdateHit()
	{
		invincibleTime = Mathf.Max(0, invincibleTime - Time.deltaTime);
		Color nextColor = spriteRenderer.color;
		if (invincibleTime > 0)
		{
			bool isFlashStrong = ((int)(invincibleTime * 15) %2) == 0;
			nextColor.a = isFlashStrong ? 0.75f : 0.25f;
		}
		else
		{
			nextColor.a = 1f;
		}
		spriteRenderer.color = nextColor;
	}
}
