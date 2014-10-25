using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour
{
	public float lifeTime = 4f;

	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
			Destroy(gameObject);
	}
}
