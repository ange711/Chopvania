using UnityEngine;
using System.Collections;

public class fallingShrooms : MonoBehaviour {

    float cooldown;
    float currTime;
    public GameObject shrooms;
    public GameObject player;
	void Start () {
        currTime = 0;
        cooldown = 5f;

	}
	
	// Update is called once per frame
	void Update () {

        currTime += 0.01f;
        Debug.Log((float)(currTime / cooldown));
        if( currTime >= cooldown)
        {
            
                Instantiate(shrooms.gameObject, gameObject.transform.position, new Quaternion());
                currTime = 0f;        
        }

        
	}
}
