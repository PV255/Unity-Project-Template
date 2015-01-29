using UnityEngine;
using System.Collections;

public class CreateFood : MonoBehaviour 
{
	int level = 1;
	public Vector3[] foodPosition;
	public Vector3[] poisonPosition;

	public GameObject food;
	public GameObject poison;
	// Use this for initialization
	void Start () 
	{
		level = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel;
		int foodNumber = 3 + level / 2;
		foodPosition = new Vector3[foodNumber];
		for (int i=0; i<foodNumber; i++) 
		{
			float px = (float) Random.value * 10 - (float) 0.5;
			float py = (float) Random.value * 10 - (float) 9.5;
			float pz = (float) Random.value * 10 - (float) 4.5;
			foodPosition[i] = new Vector3(px, py, pz);
			Instantiate(food, foodPosition[i], Quaternion.identity);
		}

		int poisonNumber = level;
		poisonPosition = new Vector3[poisonNumber];
		for (int i=0; i<poisonNumber; i++) 
		{
			float px = (float) Random.value * 10 - (float) 0.5;
			float py = (float) Random.value * 10 - (float) 9.5;
			float pz = (float) Random.value * 10 - (float) 4.5;
			poisonPosition[i] = new Vector3(px, py, pz);
			Instantiate(poison, poisonPosition[i], Quaternion.identity);
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
