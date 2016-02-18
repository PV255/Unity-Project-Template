using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class attackHintScript : MonoBehaviour {

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
            //Debug.Log("Set Attack Hint Visible");
            Image attack = GameObject.Find("attack").GetComponentInChildren<Image>();
            attack.CrossFadeAlpha(100, 1, true);
        }
    }
}
