using UnityEngine;
using System.Collections;

public class FallCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLimit>().ResetLevel();
        }
    }
}
