using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    public Transform lookAt;
    public float moveSpeed;
    public float distance;
    public float height;

    public Transform backCollider;
    
    void Start()
    {

    }

    void FixedUpdate() {
        float currDistance = Vector3.Distance(transform.position, targetTransform.transform.position);
        Debug.DrawLine(targetTransform.transform.position, transform.position, Color.red);
        transform.LookAt(targetTransform);

        //move towards the player
        //if (currDistance > distance) {
        //    Vector3 difference = targetTransform.position - transform.position;
        //    if (Mathf.Abs(difference.y) > height)
        //        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //    else
        //        transform.position += Vector3.Scale(transform.forward, new Vector3(1,0,1))
        //            * moveSpeed * Time.deltaTime;
        //}

        if (currDistance > distance) {
            transform.position += transform.forward * (currDistance - distance);
        }

        transform.LookAt(new Vector3(
            lookAt.position.x,
            lookAt.position.y + 1.5f,
            lookAt.position.z
        ));

        if (backCollider != null) {
            backCollider.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetCamera()
    {
        float camY = height;

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.up, out hitInfo, height))
        {
            camY += height - hitInfo.distance;
        }
        else if (Physics.Raycast(transform.position, transform.position + Vector3.down, out hitInfo, 20f))
        {
            camY += hitInfo.distance;
        }

        transform.position = new Vector3(targetTransform.transform.position.x,
                                        camY,
                                        targetTransform.transform.position.z - distance);
    }
}
