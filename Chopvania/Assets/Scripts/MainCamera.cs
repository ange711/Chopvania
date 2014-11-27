using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	public Transform focus;
	public float dampTime = 0.005f;
	private Vector3 velocity = Vector3.zero;
	private float vel = 0.0f;
	public bool zoomedOut = false;

	void Update(){
		if(!zoomedOut){
			Vector3 nextPos = transform.position;
			nextPos.x = focus.position.x + 2;
			float ydiff = Mathf.Abs(focus.position.y - nextPos.y);
			if(ydiff > 3){
				nextPos.y = focus.position.y;
			}
			transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, dampTime);
		}

		if(zoomedOut){
			Vector3 newPos = new Vector3(268.316f, -24.93814f, -10f);
			transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, dampTime);
			float size = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, 11.75782f, ref vel, dampTime);
			GetComponent<Camera>().orthographicSize = size;
		}
	}
}
