using UnityEngine;
using System.Collections;

public class RemovePortal : MonoBehaviour {

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
