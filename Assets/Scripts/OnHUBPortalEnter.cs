using UnityEngine;
using System.Collections;

public class OnHUBPortalEnter : MonoBehaviour {

    public string level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Application.LoadLevel(level);
        }
    }
}
