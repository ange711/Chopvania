using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	public Transform focus;
	public float dampTime = 0.005f;
	private Vector3 velocity = Vector3.zero;

	void Update(){

//		Vector3 point = camera.WorldToViewportPoint(focus.position);
//		Vector3 delta = focus.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//		Vector3 destination = transform.position + delta;
//		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

		Vector3 nextPos = transform.position;
		nextPos.x = focus.position.x + 4;
		float ydiff = Mathf.Abs(focus.position.y - nextPos.y);
		if(ydiff > 3){
			nextPos.y = focus.position.y;
		}
		transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, dampTime);
	}
}
