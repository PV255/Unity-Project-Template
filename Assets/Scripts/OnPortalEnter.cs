using UnityEngine;
using System.Collections;

public class OnPortalEnter : MonoBehaviour
{
    private GameObject levelController;

    void Start() {
        levelController = GameObject.Find("LevelController");
    }

    void OnCollisionEnter(Collision col)
    {
        levelController.GetComponent<LevelController>().GoToNextLevel();
    }
}
