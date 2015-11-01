using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float height;
    public float distance;


    private Vector3[] path;
    private float pathLength;
    Vector3 first;

    // Use this for initialization
    void Start()
    {
        //ResetCamera();
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraPath"), "speed", 5, "orienttopath", true, "EaseType", "linear", "movetopath", false));

        path = iTweenPath.GetPath("CameraPath");
        first = path[0];
        Vector3 last = path[path.Length - 1];
        pathLength = last.z - first.z;

        
    }

    // Update is called once per frame
    void Update()
    {

        float playerPos = target.transform.position.z - first.z;

        iTween.PutOnPath(this.gameObject, path, playerPos / pathLength);

        //Vector3 temp = transform.position;
        //Vector3 playerPos = target.transform.position;

        //temp.z = playerPos.z - distance;
        //temp.y = playerPos.y + height;
        //transform.position = temp;
    }

    public void ResetCamera()
    {
        transform.position = new Vector3(target.transform.position.x,
                                        height,
                                        target.transform.position.z - distance);
    }
}
