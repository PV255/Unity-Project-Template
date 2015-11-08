using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    public float moveSpeed;
    public float distance;
    public float height;


    //-------------
    //float minDistance = float.PositiveInfinity;
    //float minPercent = 0;

    // Use this for initialization
    void Start()
    {
        //ResetCamera();
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraPath"), "speed", 5, "orienttopath", true, "EaseType", "linear", "movetopath", false));

        
    }

    void FixedUpdate() {
        float currDistance = Vector3.Distance(transform.position, targetTransform.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(targetTransform.transform.position, transform.position, Color.red);
        transform.LookAt(targetTransform.transform);

        //move towards the player
        if (currDistance >= distance)
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

        //------------------
        //TODO height of camera

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, height))
        {
            transform.Rotate(targetTransform.position, 0.1f);
        }

        //Debug.Log(transform.position.y - targetTransform.position.y < height);
        //if (transform.position.y - targetTransform.position.y < height)
        //    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        

      


        //for (float t = 0; t <= 1; t += 0.005f)
        //{
        //    float dist = (target.transform.position - iTween.PointOnPath(path, t)).sqrMagnitude;
        //    if (dist < minDistance)
        //    {
        //        minDistance = dist;
        //        minPercent = t;
        //    }
        //}

        //iTween.PutOnPath(this.gameObject, path, minPercent);


        transform.LookAt(targetTransform.transform);

        //Vector3 temp = transform.position;
        //Vector3 playerPos = target.transform.position;

        //temp.z = playerPos.z - distance;
        //temp.y = playerPos.y + height;
        //transform.position = temp;
    }

    public void ResetCamera()
    {
        transform.position = new Vector3(targetTransform.transform.position.x,
                                        height,
                                        targetTransform.transform.position.z - distance);
    }
}
