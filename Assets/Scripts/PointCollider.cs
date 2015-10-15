using UnityEngine;
using System.Collections;

public class PointCollider : MonoBehaviour {

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
            this.gameObject.SetActive(false);
            other.GetComponent<PlayerLimit>().AddScore(1);
        }
    }
}
