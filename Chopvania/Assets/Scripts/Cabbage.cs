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
	public GameObject sleepParticle;
	
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


	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon" && isActive){
			disabled ();
		}
	}


	void Update(){
		if (!isActive) {
			activeTime = activeTime - Time.deltaTime;
			if(activeTime <= 0f){
				isActive = true;
				animator.SetBool ("Active", true);
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
				activeTime = Mathf.Max (0, activeTime - Time.deltaTime);
				if (activeTime <= 0f) {
					disabled();
				}
			} 
		}
	}

	void disabled(){
		GameObject sleep = (GameObject)Instantiate(sleepParticle, transform.position, Quaternion.Euler (-72f, 90f, -90f));
		isActive = false;
		activeTime = 5f; //idle time length
		timerFlag = false;
		animator.SetBool ("Active", false);
		animator.SetBool ("IsNearPlayer", false);
	}


}
