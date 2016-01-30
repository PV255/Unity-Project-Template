using UnityEngine;
using System.Collections;

public class FallRock01Collider : MonoBehaviour {

    private bool took_life;

    // Use this for initialization
    void Start () {
        took_life = false;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.collider.CompareTag("Player") && !took_life)
        {
            collision.collider.GetComponent<PlayerController>().killPlayer();
            took_life = true;
        }
    }
}
