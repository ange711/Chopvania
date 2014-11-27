using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("climbOn");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player")
		{
			collisionObject.SendMessage("climbOff");
		}
	}

}