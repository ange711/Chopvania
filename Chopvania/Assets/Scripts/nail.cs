﻿using UnityEngine;
using System.Collections;

public class nail : MonoBehaviour {
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("ApplyDamage", 1);
			collisionObject.rigidbody2D.AddForce((new Vector2(0f, 1000f)));
		}
		
	}
}
