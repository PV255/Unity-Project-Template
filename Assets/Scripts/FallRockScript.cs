using UnityEngine;
using System.Collections;

public class FallRockScript : MonoBehaviour {

    public GameObject rock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.CompareTag("Player"))
        {
            rock.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
