using UnityEngine;
using System.Collections;

public class MGCarrot : MonoBehaviour {

	public int health = 8;
	public GameObject explosion;
	public GameObject projectile;
	public GameObject player;
	float attackTimer = 4f;
	float invincibleTime = 0f;
	SpriteRenderer spriteRenderer;
	bool isFacingRight;
	Animator animator;
	
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
			if(attackTimer <= 2f){
				Attack();
				attackTimer = 3f;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject coll = col.gameObject;
		if(coll.tag == ("PlayerWeapon") && invincibleTime <= 0f){
			--health;
			invincibleTime = 0.5f;
			if(health <= 0){
				Vector2 position = new Vector2(transform.position.x, transform.position.y + 2.5f);
				Instantiate(explosion, position, Quaternion.identity);
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
















