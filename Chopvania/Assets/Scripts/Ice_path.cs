using UnityEngine;
using System.Collections;

public class Ice_path : MonoBehaviour {

	Transform playerTransform;
	GameObject Player;
	bool isFacingRight = false;
	bool onTheIce =false;
	// Use this for initialization
	void Awake()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
    void Update()
	{
		onTheIce = Mathf.Abs (transform.position.x - Player.transform.position.x) < 1;



		if (onTheIce) {

			float travelTime = 0.3f;
			travelTime -= Time.deltaTime;
			isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().isFacingRight;


			if (!isFacingRight) {

				Player.transform.Translate(new Vector3 (1, 0, 0));
		} 
			else {
				
				Player.transform.Translate(new Vector3 (-1, 0, 0));
				
			}
        }
     }
}
