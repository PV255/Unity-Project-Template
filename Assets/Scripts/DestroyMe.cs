using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {
	public GameObject food;
	public GameObject snakeprefab;
	public GameObject head;
	// Use this for initialization
	void OnTriggerEnter(Collider c)
	{
		print ("colliding");
		Destroy (this.gameObject);
		head = GameObject.Find ("Snake");
		float px = (float) Random.value*10-0.5f;
		float py = (float) Random.value*10-9.5f;
		float pz = (float) Random.value*10-4.5f;
		Vector3 foodPosition = new Vector3(px, py, pz);
		Instantiate(food, foodPosition, Quaternion.identity);
		//tu bude timer
		Vector3 NewBodyPosition = new Vector3 (c.transform.position.x, c.transform.position.y-1, c.transform.position.z);
		//Vector3 NewBodyPosition = (StoreBody.foodObject [0]);
		GameObject newbody = (GameObject) Instantiate (snakeprefab, NewBodyPosition, Quaternion.identity);
		newbody.transform.parent = head.transform;
	}
}
