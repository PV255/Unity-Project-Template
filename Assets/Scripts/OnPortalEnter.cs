using UnityEngine;
using System.Collections;

public class OnPortalEnter : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        Application.LoadLevel("testLevel01");
    }
}
