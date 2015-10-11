using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float height;
    public float distance;

    // Use this for initialization
    void Start()
    {
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        Vector3 playerPos = target.transform.position;

        temp.z = playerPos.z - distance;
        transform.position = temp;
    }

    public void ResetCamera()
    {
        transform.position = new Vector3(target.transform.position.x,
                                        target.transform.position.y + height,
                                        target.transform.position.z - distance);
    }
}
