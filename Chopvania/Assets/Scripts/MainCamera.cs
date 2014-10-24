using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	public Transform focus;

	void Update()
	{
		Vector3 nextPos = transform.position;
		nextPos.x = focus.position.x;
		transform.position = nextPos;
	}
}
