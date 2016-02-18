using UnityEngine;
using System.Collections;

public class RotationFixScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BackCollider"))
        {
            Debug.Log("Rotation Fix");
            other.GetComponent<FixRotation>().setRatotation(transform.rotation);
        }
    }
}
