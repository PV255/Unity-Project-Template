using UnityEngine;
using System.Collections;

public class OnPortalEnter : MonoBehaviour
{
    void Start() {
        
    }

    void OnCollisionEnter(Collision col)
    {
        GameManager.Instance.GetComponent<GameManager>().GoToNextLevel();
    }
}
