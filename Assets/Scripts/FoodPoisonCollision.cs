using UnityEngine;
using System.Collections;

// If a food object is created at the coordinates of an existing poison object, this script deletes it and creates a new one somewhere else
public class FoodPoisonCollision : MonoBehaviour 
{	
	public GameObject food;
	
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "Poison") 
		{
			Destroy(this.gameObject);
			
			float px = (float) Random.value * 10 - 0.5f;
			float py = (float) Random.value * 10 - 9.5f;
			float pz = (float) Random.value * 10 - 4.5f;
			
			Vector3 foodPosition = new Vector3(px, py, pz);
			
			Instantiate(food, foodPosition, Quaternion.identity);
		}
	}
}
