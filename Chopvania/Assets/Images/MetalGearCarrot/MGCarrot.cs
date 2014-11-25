using UnityEngine;
using System.Collections;

public class MGCarrot : MonoBehaviour {

	public int health = 8;
	public GameObject explosion;
	public GameObject projectile;
	public GameObject player;

	float attackTimer = 6f;
	float invincibleTime = 0f;
	SpriteRenderer spriteRenderer;
	bool isFacingRight;
	Animator animator;

	bool down;
	bool attackedOnce = false;
	bool attackedTwice = false;
	bool isDown = false;
	int currentPos = 1;

	public GameObject surprise;
	public GameObject dirtPile;
	public GameObject grass1;
	public GameObject grass2;
	
	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		
	}
	
	void Update (){
		if(invincibleTime > 0f){
			invincibleTime -= Time.deltaTime;
			UpdateInvincibility();
		}


		if(animator.GetBool("isAngry")){
			attackTimer -= Time.deltaTime;
			UpdateAttackSequence();
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject coll = col.gameObject;
		if (coll.tag == "Player" && !coll.GetComponent<Hero>().IsInvincible()){
			coll.SendMessage("ApplyDamage", 1);
		}
		if(coll.tag == ("PlayerWeapon") && invincibleTime <= 0f){
			--health;
			invincibleTime = 0.5f;
			if(health <= 0){
				Vector2 position = new Vector2(transform.position.x, transform.position.y + 2.5f);
				Instantiate(explosion, position, Quaternion.identity);
				if(currentPos == 1){
					Instantiate (dirtPile, grass1.transform.position, Quaternion.identity);
					Destroy (grass1);
				}
				if(currentPos == 2){
					Instantiate (dirtPile, grass2.transform.position, Quaternion.identity);
					Destroy (grass2);
				}

				Destroy (gameObject);

			}
		}
	}
	
	void UpdateInvincibility(){
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

	void UpdateAttackSequence(){
		if(attackTimer <= 5f && !attackedOnce){
			Attack();
			attackedOnce = true;
		}
		if(attackTimer <= 3f && !attackedTwice){
			Attack();
			attackedTwice = true;
		}
		if(attackTimer <= 1f && !isDown){
			Vector2 newPos = transform.position;
			newPos.y -= 15f;
			transform.position = newPos;
			newPos.y += 19f;
			GameObject a = (GameObject)Instantiate(surprise, newPos, Quaternion.identity);
			a.AddComponent("TimedDeath");
			a.GetComponent<TimedDeath>().lifeTime = 0.75f;
			isDown = true;
		}
		if(attackTimer <= 0 && isDown){
			if(currentPos == 1){
				transform.position = new Vector3(-0.0695942f, -3.775169f, 0.1f);
				currentPos = 2;
			}
			else if(currentPos == 2){
				transform.position = new Vector3(18.40829f, -3.775169f, 0.1f);
				currentPos = 1;
			}
			attackedOnce = false;
			attackedTwice = false;
			isDown = false;
			attackTimer = 6f;

		}
	}

	void Attack(){
		Vector2 position = new Vector2(transform.position.x, transform.position.y + 2f);
		position.x += Mathf.Sign(player.transform.position.x - transform.position.x) * 1.9f;
		GameObject car = (GameObject)Instantiate(projectile, position, Quaternion.identity);
		if((player.transform.position.x - transform.position.x) <= 1){
			car.GetComponent<Carrot>().Flip();
		}
		car.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * 200f,0f));
	}

}
















