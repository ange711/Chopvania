using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour
{
	public bool isOnFloor;

	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isOnFloor && Input.GetButtonDown("Fire2")){
			collisionObject.SendMessage ("pickUpBar");
			Destroy(gameObject);
		}

	}

}
