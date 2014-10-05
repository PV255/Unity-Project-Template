using UnityEngine;
using System.Collections;

public class CreateFood : MonoBehaviour {
	int level = 1;
	public Vector3[] foodPosition;

	public GameObject food;
	// Use this for initialization
	void Start () {
		int foodNumber = 5 - level / 2;
		foodPosition = new Vector3[foodNumber];
		for (int i=0; i<foodNumber; i++) {
			float px = (float) Random.value*10- (float) 0.5;
			float py = (float) Random.value*10- (float) 9.5;
			float pz = (float) Random.value*10- (float) 4.5;
			foodPosition[i]=new Vector3(px, py, pz);
			Instantiate(food);

				}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
