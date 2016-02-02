using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class jumpHintScript : MonoBehaviour {

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
            Debug.Log("Set Jump Hint Visible");
            Image jump = GameObject.Find("jump").GetComponentInChildren<Image>();
            jump.CrossFadeAlpha(100, 1, true);
            //Image jump = GameObject.Find("CanvasHints").GetComponentInChildren<Image>();
        }
    }
}
