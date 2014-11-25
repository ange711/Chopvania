using UnityEngine;
using System.Collections;

public class Pie : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player") {
			collisionObject.SendMessage ("EatPie");
			Destroy (gameObject);
		}
	}

}