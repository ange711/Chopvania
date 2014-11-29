using UnityEngine;
using System.Collections;

public class ScenePlayer : MonoBehaviour {

	public GameObject[] scenes;
	public string level;
	int currentScene = 0;
	bool set = false;
	
	// Update is called once per frame
	void Update () {
		if(!set){
			set = true;
			Instantiate(scenes[currentScene]);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			set = false;
			++currentScene;
			if(currentScene >= scenes.Length){
				Application.LoadLevel(level);
			}
		}
	
	}
}
