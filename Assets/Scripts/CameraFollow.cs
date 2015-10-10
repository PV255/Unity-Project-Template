using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public float height;
    public float distance;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(target.transform.position.x, 
                                        target.transform.position.y + height,
                                        target.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = transform.position;
        temp.z = target.transform.position.z - distance;
        transform.position = temp;
	}
}
