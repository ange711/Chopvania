using UnityEngine;
using System.Collections;

public class TimedHelp : MonoBehaviour {

	public float lifeTime = 4f;
	public GameObject arrow;
	
	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f){
			Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 3f);
			Quaternion rotation = Quaternion.Euler(0f,180f,180f);
			Instantiate(arrow, newPos, rotation);

		}
	}
}
