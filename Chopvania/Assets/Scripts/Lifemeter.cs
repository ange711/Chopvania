using UnityEngine;
using System.Collections;

public class Lifemeter : MonoBehaviour
{
	public GameObject initialBar;
	int BAR_COUNT = 10;
	int life;
	GameObject[] bars;

	void Start()
	{
		bars = new GameObject[BAR_COUNT];
		life = BAR_COUNT;
		Vector3 nextPosition = initialBar.transform.position;
		bars[0] = initialBar;
		for (int i = 1; i < BAR_COUNT; i++)
		{
			nextPosition.x += 0.035f;
			var bar = (GameObject)Instantiate(initialBar);
			bar.transform.parent = initialBar.transform.parent;
			bar.transform.position = nextPosition;
			bars[i] = bar;
		}
	}

	public int DecrementLife(int amount)
	{
		life = Mathf.Max(0, life - amount);
		UpdateBars();
		return life;
	}

	public int IncrementLife(int amount)
	{
		life = Mathf.Min(10, life + amount);
		UpdateBars();
		return life;
	}

	void UpdateBars()
	{
		for (int i = 0; i < BAR_COUNT; i++)
		{
			bars[i].SetActive(i < life);
		}
	}
}
