using UnityEngine;
using System.Collections;

public class WhereIsMyParent : MonoBehaviour {
	Vector3 currentPosition;
	Vector3 previousPosition;
	bool isMyParentHead;
	GameObject headIfParentHead;
	GameObject headIfParentNotHead;
	int timer = 0;
	// Use this for initialization
	void Start () {
		transform.position = transform.parent.position;
		currentPosition = transform.position;
		headIfParentHead = GameObject.Find ("Snake");
		headIfParentNotHead = transform.parent.gameObject;
		isMyParentHead = (headIfParentHead.Equals( headIfParentNotHead));
		print ("parent: " + headIfParentNotHead.name + " head: " + headIfParentHead.name);
		if (isMyParentHead) {
			print ("my parent is head");
		} else {
			print("my parent is not head");

		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (timer > 20) {
			previousPosition = currentPosition;
			if (isMyParentHead) {
				//print("my parent is head");
				WhereIAM pos = headIfParentHead.GetComponent <WhereIAM> ();
				currentPosition = pos.returnPreviousPosition ();
				this.transform.position = currentPosition;
				/*WhereIAM head = GetComponent<WhereIAM>(GameObject.Find ("Snake"));
			currentPosition = head.returnPreviousPosition();*/
				//currentPosition = GameObject.Find ("Snake").re
			} else {
				//print("my parent is not head");
				WhereIsMyParent p = headIfParentNotHead.GetComponent <WhereIsMyParent> ();
				currentPosition = p.returnPreviousBodyPosition ();
				this.transform.position = currentPosition;
			}
		} else {
			timer++;
		}
	}

	public Vector3 returnPreviousBodyPosition ()
	{
		return previousPosition;
		}
}
