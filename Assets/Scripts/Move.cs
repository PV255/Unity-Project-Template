using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour {
	private GestureListener gestureListener; //gesture listener for Kinect
	public float speed = 1.0F;
	public Vector3 change;
	//public float rotateSpeed = 3.0F;
	void Start(){
		speed = speed+(GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel)*1.5F;
		gestureListener = Camera.main.GetComponent<GestureListener>();
		}

	void Update () {

		KinectManager kinectManager = KinectManager.Instance;


		// The step size is equal to speed times frame time.
		var step = speed * Time.deltaTime;
		
		// Move our position a step closer to the target.
		Vector3 target = transform.position;

		if (Input.GetKey ("up")){
						//target.y++;
						change.Set(0,step,0);

				}
			
		
		if (Input.GetKey ("down")) {
						//target.y--;
						change.Set (0, -step, 0);
				}

		if (Input.GetKey ("left")) {
						//target.x++;
						change.Set (step, 0, 0);
				}
		
		if (Input.GetKey ("right")) {
						//target.x--;
						change.Set (-step, 0, 0);
				}

		if (Input.GetKey ("f")) {
						//target.z++;
						change.Set (0, 0, step);
				}
		
		if (Input.GetKey ("b")) {
						//target.z--;
						change.Set (0, 0, -step);
				}

		if (gestureListener) {
			if (gestureListener.IsSwipeLeft ()) {
				change.Set (step, 0, 0);
				//print ("gesture left swipe");
			}
			if (gestureListener.IsSwipeRight ()) {
				change.Set (-step, 0, 0);
				//print ("gesture right swipe");
			}
			if (gestureListener.IsSwipeUp ()) {
				change.Set (0, step, 0);
				//print ("gesture up swipe");
			}
			if (gestureListener.IsSwipeDown ()) {
				change.Set (0, -step, 0);
				//print ("gesture down swipe");
			}
			if(gestureListener.IsPull()){
				change.Set(0, 0, step);
				//print ("gesture pull");
			}
			if(gestureListener.IsPush()){
				change.Set(0, 0, -step);
				//print ("gesture push");
			}

		}



		target = target+change;

		//not to go out of the plane

	//	var gamePlanePosition = GameObject.Find("GamePlane").transform.position;
		if ((target.x <9.5) && (target.x > -0.5) && (target.y > -9.5) && (target.y <0.5) && (target.z <5.5) && (target.z > -4.5)) {
		//Debug.Log("target.x " + target.x + "/n gamePlanePosition " + gamePlanePosition + "target.y " + target.y + "target.z " + target.z);
						transform.position = Vector3.MoveTowards (transform.position, target, step);
				}
	}
}