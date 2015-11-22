using UnityEngine;
using System.Collections;

public class FallCP : MonoBehaviour {

    private GameObject mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLimit>().SetStartPos(other.transform.position, mainCamera.transform.position);
        }
    }
}
