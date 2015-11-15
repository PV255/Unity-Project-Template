using UnityEngine;
using System.Collections;

public class PointCollider : MonoBehaviour {

    private GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            gameManager.GetComponent<GameManager>().AddScore(1);
        }
    }
}
