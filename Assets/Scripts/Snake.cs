using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

    public float moveTime;
    public float moveRate;
    public float moveDistance;
    Vector2 dir = Vector2.right;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Move", moveTime, moveRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move() {
        //TODO move
        transform.Translate(dir * moveDistance);
    }
}
