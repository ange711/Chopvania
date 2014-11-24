using UnityEngine;
using System.Collections;

public class OnionLayer : MonoBehaviour
{
	public int damage = 1;

	void Start () {
		Destroy(gameObject, 5f);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible())
		{
			collisionObject.SendMessage("ApplyDamage", damage);
			Destroy(gameObject);
		}
	}
}
