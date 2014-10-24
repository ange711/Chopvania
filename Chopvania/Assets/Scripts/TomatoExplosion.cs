using UnityEngine;
using System.Collections;

public class TomatoExplosion : MonoBehaviour
{
	public int damage = 5;
	float activeTime = 0.35f;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
		}
	}

	void Update(){
		Invoke ("Remove", activeTime);
	}

	void Remove(){
		Destroy(gameObject);
	}
}
