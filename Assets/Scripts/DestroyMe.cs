using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider c)
	{
		print ("colliding");
		Destroy (this.gameObject);

	}
}
