using UnityEngine;
using System.Collections;

public class Carrot : MonoBehaviour {

	int damage = 2;

	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;		
		if (collisionObject.tag == "Player" && !collisionObject.GetComponent<Hero>().IsInvincible()){
			collisionObject.SendMessage("ApplyDamage", damage);
		}
		Destroy (gameObject);
	}

	public void Flip(){
		Vector3 nextScale = transform.localScale;
		nextScale.x *= -1f;
		transform.localScale = nextScale;
	}
}
