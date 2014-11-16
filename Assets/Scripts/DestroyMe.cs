using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour 
{
	public GameObject food;
	public GameObject snakeprefab;
	public GameObject head;
	public GameObject parent;
	void Start()
	{
		head = GameObject.Find ("Snake");
		}
	// Use this for initialization
	void OnTriggerEnter(Collider c)
	{
		print ("colliding");
		Destroy (this.gameObject);


		float px = (float) Random.value*10-0.5f;
		float py = (float) Random.value*10-9.5f;
		float pz = (float) Random.value*10-4.5f;
		Vector3 foodPosition = new Vector3(px, py, pz);
		Instantiate(food, foodPosition, Quaternion.identity);
		print ("created food");
		//tu bude timer
		//Vector3 NewBodyPosition = new Vector3 (c.transform.position.x, c.transform.position.y-1, c.transform.position.z);
		WhereIAM pos = head.GetComponent <WhereIAM> ();
		Vector3 NewBodyPosition = pos.returnPreviousPosition ();
		GameObject newbody = (GameObject) Instantiate (snakeprefab, NewBodyPosition, Quaternion.identity);
		print ("created body");
		newbody.transform.parent = FindSon(head.transform);
		print ("created body with parent " + newbody.transform.parent);
	}
	public Transform FindSon(Transform oldParent)
	{



			if(transform.childCount > 0) {
						Transform son = oldParent.transform.GetChild (1);
						return FindSon (son);
						
				} else {
						return oldParent.transform;
				}
		}
}
