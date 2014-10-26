using UnityEngine;
using System.Collections;

public class Cabbage : MonoBehaviour
{
	public float triggerDistance = 2f;
	bool isNearPlayer = false;
	float activeTime = 2f;
	bool isActive;
	bool timerFlag;
	Animator animator;
	GameObject player;
	
	void Awake()
	{
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		isActive = true;
		timerFlag = false;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "Player" && isActive)
		{
			collisionObject.SendMessage("ApplyDamage", 1);
		}
	}


	void Update(){
		if (!isActive) {
			activeTime = activeTime - Time.deltaTime;
			if(activeTime <= 0f){
				isActive = true;
				activeTime = 2f; //dangerous time length
			}
		}
	
		if (isActive) {
			isNearPlayer = (Mathf.Abs(transform.position.x - player.transform.position.x) < triggerDistance);
			if (isNearPlayer) {
				animator.SetBool ("IsNearPlayer", true);
				timerFlag = true;
			}
			if (timerFlag) {
				//Can Apply damage if peircing box collider, but must remain solid, how?
				activeTime = Mathf.Max (0, activeTime - Time.deltaTime);
				if (activeTime <= 0f) {
					isActive = false;
					activeTime = 5f; //idle time length
					timerFlag = false;
					animator.SetBool ("IsNearPlayer", false);
				}
			} 
		}

	}

}
