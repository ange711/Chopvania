using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	
	public int health = 2;
	float spawnTime = 3f;
	float tentacleTime = 3f;
	float playerPosition = 0f;
	public GameObject explosion;
	GameObject player;
	//BoxCollider2D boxcollider;
	PolygonCollider2D polyCollider;
	public GameObject spawn;
	public GameObject tentacle;
	bool idle = true;
	bool damageable = true;
	int damage = 4;
	float invincibleTime;
	SpriteRenderer spriteRenderer;
	Vector2 position = new Vector2(34.34713f, -41.90192f);
	
	void Awake(){
		//boxcollider = GetComponent<BoxCollider2D>();
		polyCollider = GetComponent<PolygonCollider2D> ();
		player = GameObject.FindGameObjectWithTag("Player");
		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}
	
	void Update () {
		UpdateHit ();
		if (Mathf.Abs (player.transform.position.x - transform.position.x) < 10f) 
		{
			idle = false;
			GetComponent<Animator> ().SetBool ("playerClose", true);
			damageable = true;
		}
		else 
		{
			GetComponent<Animator> ().SetBool ("playerClose", false);
			damageable = false;
		}
		
		if(!idle)
		{
			updateSpawning();
		}
		
	}

	void updateSpawning()
	{
		spawnTime = Mathf.Max(0, spawnTime - Time.deltaTime);
		tentacleTime = Mathf.Max(0, tentacleTime - Time.deltaTime);
		if(spawnTime <= 0)
		{
			var o = (GameObject)Instantiate (spawn, position, Quaternion.identity);
			spawnTime = 6f;
		}
		if (tentacleTime <= 0)
		{
			float prevPos = playerPosition;
			playerPosition = player.transform.position.x;
			if(Mathf.Abs(prevPos - playerPosition) <= 1)
			{
				var o = (GameObject)Instantiate (tentacle, player.transform.position, Quaternion.identity);
			}
			tentacleTime = 5f;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon" && damageable){
			--health;
			invincibleTime = 0.5f;
			if(health == 0){
				Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
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

