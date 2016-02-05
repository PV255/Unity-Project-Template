using UnityEngine;
using System.Collections;

public class ItemLife : MonoBehaviour {
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.AddLife();
        }
    }
}
