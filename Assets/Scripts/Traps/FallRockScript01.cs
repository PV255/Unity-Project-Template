using UnityEngine;
using System.Collections;

public class FallRockScript01 : MonoBehaviour {

    public GameObject rock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = rock.GetComponent<Rigidbody>();
            Vector3 dir = new Vector3(500, -50, 500);
            dir = dir.normalized;
            rb.AddForce(dir * 500f);
            rb.useGravity = true;
        }
    }
}
