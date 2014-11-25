using UnityEngine;
using System.Collections;

public class BossCamera : MonoBehaviour
{
	
	public Transform focus;
	public float dampTime = 0.005f;
	private Vector3 velocity = Vector3.zero;
	private float vel = 0.0f;
	public bool zoomedOut = false;
	
	void Update(){
		if(!zoomedOut){
			Vector3 nextPos = transform.position;
			nextPos.x = focus.position.x + 4;
			float ydiff = Mathf.Abs(focus.position.y - nextPos.y) +0.75f;
			if(ydiff > 3){
				nextPos.y = focus.position.y-1;
			}
			transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, dampTime);
		}
		
		if(zoomedOut){
			Vector3 newPos = new Vector3(7.91263f, -25.15001f, -10f);
			transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, dampTime);
			float size = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, 24.68899f, ref vel, dampTime);
			GetComponent<Camera>().orthographicSize = size;
		}
	}
}
