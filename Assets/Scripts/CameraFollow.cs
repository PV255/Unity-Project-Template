using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    public Transform lookAt;
    public float moveSpeed;
    public float distance;
    public float height;


    
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
            Debug.Log(transform.forward);
            transform.position += transform.forward * (currDistance - distance);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetCamera()
    {
        float camY = height;
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.yellow);

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
