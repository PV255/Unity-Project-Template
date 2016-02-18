using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

    Quaternion rotation;

    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setRatotation(Quaternion rotation) {
        this.rotation = rotation;
    }
}
