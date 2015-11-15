using UnityEngine;
using System.Collections;

public class OnPortalEnter : MonoBehaviour
{
    private GameObject gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager");
    }

    void OnCollisionEnter(Collision col)
    {
        gameManager.GetComponent<GameManager>().GoToNextLevel();
    }
}
