using UnityEngine;
using System.Collections;

public class FallRock01Collider : MonoBehaviour {

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.collider.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().DestroyLife();
        }
    }
}
