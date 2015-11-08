using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    public float moveSpeed;
    public float distance;
    public float height;


    
    void Start()
    {
        
    }

    void FixedUpdate() {
        float currDistance = Vector3.Distance(transform.position, targetTransform.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(targetTransform.transform.position, transform.position, Color.red);
        transform.LookAt(targetTransform.transform);

        //move towards the player
        if (currDistance > distance) {
            Vector3 difference = targetTransform.position - transform.position;
            if (Mathf.Abs(difference.y) > height)
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            else
                transform.position += Vector3.Scale(transform.forward, new Vector3(1,0,1))
                    * moveSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetTransform.transform);
    }

    public void ResetCamera()
    {
        transform.position = new Vector3(targetTransform.transform.position.x,
                                        height,
                                        targetTransform.transform.position.z - distance);
    }
}
