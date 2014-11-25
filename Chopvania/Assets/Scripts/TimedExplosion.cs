using UnityEngine;
using System.Collections;

public class TimedExplosion : MonoBehaviour {

	public float lifeTime = 2f;
	public GameObject explosion;
	
	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0){
			Vector2 wall = new Vector2(transform.position.x, transform.position.y);
			var explode = (GameObject) Instantiate(explosion, wall, Quaternion.identity);
			Instantiate(explosion, transform.position, Quaternion.identity);
			explode.transform.localScale = new Vector3(9.0f, 9.0f, 2.0f);
			Destroy(gameObject);
		}
	}
}
