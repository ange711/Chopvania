using UnityEngine;
using System.Collections;

public class SlantCamera : MonoBehaviour
{
	
	public Transform focus;
	public float dampTime = 0.005f;
	private Vector3 velocity = Vector3.zero;
	
	void Update(){
		Vector3 nextPos = transform.position;
		nextPos.x = focus.position.x + 4;
		float ydiff = Mathf.Abs(focus.position.y - nextPos.y);
		if(ydiff > 1){
			nextPos.y = focus.position.y;
		}
		transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, dampTime);
	}
}
