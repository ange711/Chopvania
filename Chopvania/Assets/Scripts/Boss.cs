using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	
	int health = 5;
	float spawnTime = 3f;
	float tentacleTime = 3f;
	public GameObject explosion;
	GameObject player;
	public GameObject spawn;
	public GameObject tentacle;
	public GameObject star;
	bool idle = true;
	bool damageable = true;
	int damage = 2;
	float invincibleTime;
	SpriteRenderer spriteRenderer;
	float pos;
	float offSet;
	
	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}
	
	void Update () {
		UpdateHit ();
		if (Mathf.Abs (player.transform.position.x - transform.position.x) < 30f) 
		{
			idle = false;

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
			Vector2 position = new Vector2(transform.position.x - 1f, transform.position.y);
			Instantiate (spawn, position, Quaternion.identity);
			spawnTime = 6f;
		}
		if (tentacleTime <= 0)
		{
			pos = player.transform.position.x;
			offSet = health;
			Instantiate (star, new Vector3(pos, -19f, 5f), Quaternion.identity);
			Instantiate (star, new Vector3(pos + offSet, -19f, 5f), Quaternion.identity);
			Instantiate (star, new Vector3(pos - offSet, -19f, 5f), Quaternion.identity);
			Invoke("tentacleAttack", 1.5f);
			tentacleTime = 5f;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon" && damageable){
			health--;
			damageable = false;
			invincibleTime = 2f;
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			health--;
			invincibleTime = 2f;
		}
		if(health == 0){
			Instantiate(explosion, transform.position, Quaternion.identity);
			GameObject.Find("EndCredits").AddComponent("TimedLevelSwitch");
			GameObject.Find("EndCredits").GetComponent<TimedLevelSwitch>().levelName = "Ending";
			Destroy(gameObject);
		}
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
		}
	}

	public void MinionMinus(){

	}

	void tentacleAttack(){
		Instantiate (tentacle, new Vector3(pos, -19f, 5f), Quaternion.identity);
		Instantiate (tentacle, new Vector3(pos + offSet, -19f, 5f), Quaternion.identity);
		Instantiate (tentacle, new Vector3(pos - offSet, -19f, 5f), Quaternion.identity);
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
			damageable = true;
			nextColor.a = 1f;
		}
		spriteRenderer.color = nextColor;
	}
	
}

