using UnityEngine;
using System.Collections;

public class LavaCollision : MonoBehaviour {

	public void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            GameObject player = col.gameObject;
            player.GetComponent<PlayerController>().killPlayer();
            player.GetComponent<PlayerController>().setDescending(true);
        }
    }
}