using UnityEngine;
using System.Collections;

public class FallCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GetComponent<GameManager>().DestroyLife();
            other.GetComponent<PlayerLimit>().ResetLevel();
        }
    }
}
