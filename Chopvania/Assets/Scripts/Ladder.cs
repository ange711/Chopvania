using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
	//bool isNearPlayer = false;
	//float activeTime = 2f;
	bool isActive;
	//bool timerFlag;
	//Animator animator;
	//GameObject player;
	
	void Awake()
	{
		//animator = GetComponent<Animator>();
		//player = GameObject.FindGameObjectWithTag("Player");
		isActive = true;
		//timerFlag = false;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isActive)
		{
			collisionObject.SendMessage("climbMode");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isActive)
		{
			collisionObject.SendMessage("climbOff");
		}
	}

}