using UnityEngine;
using System.Collections;

public class BoxTimedDeath : MonoBehaviour {

	float lifeTime = 1f;
	SpriteRenderer spriteRenderer;
	GameObject MGCarrot;
	
	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		MGCarrot = GameObject.Find("MetalGearCarrot");
	}

	void Update()
	{
		lifeTime -= Time.deltaTime;

		Color nextColor = spriteRenderer.color;
		bool isFlashStrong = ((int)(lifeTime * 15) %2) == 0;
		nextColor.a = isFlashStrong ? 0.75f : 0.25f;
		spriteRenderer.color = nextColor;
		if (lifeTime <= 0){
			Destroy(gameObject);
			MGCarrot.GetComponent<Animator>().SetBool("isAngry",true);
		}

	}
}
