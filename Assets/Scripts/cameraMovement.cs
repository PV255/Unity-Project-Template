using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {
	public float scrollSpeed = 10f;
	public float speedIncr = 5f;
	public float zoomSpeed = 200f;
	public int edgeLimit = 50;
	public GameObject dofTarget; //Empty object at the terrain height parented to the camera
    private int player = 1;
    private Vector3 lastPosition;

    public void reset()
    {
        player = player == 1 ? -1 : 1;
    }

    void Update() {
        //
        if(transform.position.x < -6 || transform.position.x > 30 || transform.position.z < -20 || transform.position.z > 50)
        {
            transform.position = lastPosition;
        }
        lastPosition = transform.position;

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			scrollSpeed += speedIncr;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			scrollSpeed -= speedIncr;
		}
		//EDGE SCROLL
		if (!Input.GetMouseButton (0)) {
			if (Input.mousePosition.y > Screen.height - edgeLimit) {
				Move (Vector3.forward * player, scrollSpeed);
			}
			if (Input.mousePosition.y < edgeLimit) {
				Move (Vector3.back * player, scrollSpeed);
			}
			if (Input.mousePosition.x < edgeLimit) {
				Move (Vector3.left * player, scrollSpeed);
			}
			if (Input.mousePosition.x > Screen.width - edgeLimit) {
				Move (Vector3.right * player, scrollSpeed);
			}
		} else {
			//CLICK AND DRAG
			transform.Translate(-1 * player * new Vector3(Input.GetAxis ("Mouse X")*scrollSpeed*Time.deltaTime, 0, Input.GetAxis ("Mouse Y")*player*scrollSpeed * Time.deltaTime), Space.World);
		}
		//ZOOM BEGIN
		transform.Translate (new Vector3(0, 0, Input.GetAxis ("Mouse ScrollWheel")*zoomSpeed*Time.deltaTime), Space.Self);
		dofTarget.transform.Translate (new Vector3(0, 0, -Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime), Space.Self);
		//ZOOM END
		//WSAD or ARROWS
		transform.Translate (new Vector3(Input.GetAxis("Horizontal")*scrollSpeed * player * Time.deltaTime, 0, Input.GetAxis("Vertical")*scrollSpeed * player * Time.deltaTime), Space.World);
	}

	void Move(Vector3 vector, float speed){
		transform.Translate (vector*speed*Time.deltaTime, Space.World);
	}
}
