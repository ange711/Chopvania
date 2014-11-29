using UnityEngine;
using System.Collections;

public class TimedLevelSwitch : MonoBehaviour
{
	public float lifeTime = 2f;
	public string levelName;
	
	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
			Application.LoadLevel(levelName);
	}
}
