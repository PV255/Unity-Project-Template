using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class innerBallGravity : MonoBehaviour {

    public List<GameObject> objects;
    public GameObject planet;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
        planet = GameObject.Find("sphere");
    }

    void Update()
    {
        Vector3 direction = planet.transform.position - transform.position;
        planet.transform.Rotate(transform.forward * 10);
    }
    
}
