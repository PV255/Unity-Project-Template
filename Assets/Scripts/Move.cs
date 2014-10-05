using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour {
	public float speed = 3.0F;
	//public float rotateSpeed = 3.0F;
	void Update () {
		// The step size is equal to speed times frame time.
		var step = speed * Time.deltaTime;
		
		// Move our position a step closer to the target.
		Vector3 target = transform.position;
		if (Input.GetKey ("up"))
			target.y++;
		
		if (Input.GetKey ("down"))
			target.y--;

		if (Input.GetKey ("left"))
			target.x++;
		
		if (Input.GetKey ("right"))
			target.x--;

		if (Input.GetKey ("f"))
			target.z++;
		
		if (Input.GetKey ("b"))
			target.z--;

		//not to go out of the plane

		var gamePlanePosition = GameObject.Find("GamePlane").transform.position;
		if ((target.x <9.5) && (target.x > -0.5) && (target.y > -9.5) && (target.y <0.5) && (target.z <5.5) && (target.z > -4.5)) {
		//Debug.Log("target.x " + target.x + "/n gamePlanePosition " + gamePlanePosition + "target.y " + target.y + "target.z " + target.z);
						transform.position = Vector3.MoveTowards (transform.position, target, step);
				}
	}
}