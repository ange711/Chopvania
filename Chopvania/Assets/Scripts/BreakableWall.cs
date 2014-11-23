using UnityEngine;
using System.Collections;

public class BreakableWall : MonoBehaviour
{
	public GameObject wallParticle;

	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Enemy") {
			Instantiate (wallParticle, new Vector3(transform.position.x, transform.position.y + 1.5f, -10f), Quaternion.identity);
			Destroy (gameObject);
			collisionObject.SendMessage ("Explode");
		}
	}
}
