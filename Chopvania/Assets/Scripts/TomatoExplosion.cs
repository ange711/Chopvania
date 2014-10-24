using UnityEngine;
using System.Collections;

public class TomatoExplosion : MonoBehaviour
{
	public int damage = 5;
	float activeTime = 0.45f;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
		}
	}

	void Update(){
		activeTime = activeTime - Time.deltaTime;
		if(activeTime <= 0f){
			Destroy(gameObject);
		}
	}
}
