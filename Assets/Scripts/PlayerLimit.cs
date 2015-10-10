using UnityEngine;
using System.Collections;

public class PlayerLimit : MonoBehaviour
{

    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.y);
        if (transform.position.y < -5)
        {
            transform.position = startPos;
        }
    }
}
