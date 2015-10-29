using UnityEngine;
using System.Collections;

public class cstmControllerCamera : MonoBehaviour {
    public GameObject target;

    public Transform[] nodes;
    int currentNode;
    Vector3 currentCamPosition;

    // Use this for initialization
    void Start(){
        currentNode = 0;
        currentCamPosition = nodes[currentNode].position;
    }

    // Update is called once per frame
    void Update(){
        updateCurrentNode();

        float altitudeBase = Mathf.Min(nodes[currentNode + 1].position.y, nodes[currentNode].position.y);

        Vector3 tpos = target.transform.position;

        RaycastHit res;
        Physics.Raycast(tpos, Vector3.down, out res, 1337);

        transform.forward = nodes[currentNode + 1].position - nodes[currentNode].position;
        transform.position = res.point - transform.forward * 8 + Vector3.up * altitudeBase;







        //Vector3 currentDirection = nodes[currentNode + 1].position - nodes[currentNode].position;
        //float currentNodeDist = Vector3.Magnitude(currentDirection);

        //float altitudeBase = Mathf.Min(nodes[currentNode + 1].position.y, nodes[currentNode].position.y);
        //float altitudeDiff = Mathf.Abs(nodes[currentNode + 1].position.y - nodes[currentNode].position.y);

        //RaycastHit res;
        //Physics.Raycast(target.transform.position, Vector3.down, out res, 1337);

        //Vector3 newCamPosition = new Vector3(res.point.x, altitudeBase, res.point.z) - new Vector3(currentDirection.x, altitudeBase, currentDirection.z) * 3;

        //float someDist = Vector3.Magnitude(newCamPosition - nodes[currentNode].position);
        //newCamPosition.y += (someDist / currentNodeDist) * altitudeDiff;

        ////currentCamPosition = newCamPosition;
        ////transform.position = currentCamPosition;

        //transform.forward = currentDirection;
    }

    private static float delta = 2;

    private void updateCurrentNode()
    {
        if (nodes.Length > 5){
            return;
        }

        if (currentNode == 0){
            if (Vector3.Magnitude(nodes[currentNode + 1].position - currentCamPosition) < delta){
                currentNode++;
                return;
            }
        }

        if (currentNode == nodes.Length - 2){
            if (Vector3.Magnitude(nodes[currentNode - 1].position - currentCamPosition) < delta){
                currentNode--;
                return;
            }
        }

        if (Vector3.Magnitude(nodes[currentNode + 1].position - currentCamPosition) < delta)
        {
            currentNode++;
        }

        if (Vector3.Magnitude(nodes[currentNode - 1].position - currentCamPosition) < delta)
        {
            currentNode--;
        }
    }
}
