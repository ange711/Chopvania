﻿using UnityEngine;
using System.Collections;

public class Cotainer : MonoBehaviour {
	
	public GameObject enemry;
	public int life = 1;
	
	
	Transform playerTransform;
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero> ().IsInvincible())
		{
			Instantiate(enemry, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else if (collisionObject.tag == "PlayerWeapon" /*&& collisionObject.GetComponent<PlayerBullet>().isActive*/)
		{
			Instantiate(enemry, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	
}