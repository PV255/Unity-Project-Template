using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move2 : MonoBehaviour 
{
	private GestureListener gestureListener; //gesture listener for Kinect
	public int snakeLength = 3;
	public float speed = 0.05f;
	private int lr = 0; // x axis increment
	private int ud = 0; // z axis increment
	private int fb = 0; // y axis increment
	private bool init = true;

	// Use this for initialization
	void Start () 
	{
		gestureListener = Camera.main.GetComponent<GestureListener>();

		InvokeRepeating("Move", 0.1f, speed);
	}

	public void Move()
	{
		Vector3 pos = transform.position;
		Vector3 oldPos = transform.position;
		pos.x += lr;
		pos.y += ud;
		pos.z += fb;
		transform.position = pos;

		if (transform.position != oldPos) 
		{
			for (int i = snakeLength; i > 2; i--) 
			{
				GameObject.Find("snake" + i).gameObject.transform.position = GameObject.Find("snake" + (i-1)).gameObject.transform.position;
			}
			GameObject.Find ("snake2").gameObject.transform.position = oldPos;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		KinectManager kinectManager = KinectManager.Instance;

		if (Input.GetKey ("left")) 
		{
			if (lr == 0) 
			{
				lr = 1;
				ud = 0;
				fb = 0;
				if (init) 
				{
					init = false;
				}
			}
		}		
		if (Input.GetKey ("right") && !init) 
		{
			if (lr == 0) 
			{
				lr = -1;
				ud = 0;
				fb = 0;
			}
		}	
		if (Input.GetKey ("up"))
		{	
			if (ud == 0) 
			{
				lr = 0;
				ud = 1;
				fb = 0;
				if (init) 
				{
					init = false;
				}
			}
		}		
		if (Input.GetKey ("down")) 
		{
			if (ud == 0) 
			{
				lr = 0;
				ud = -1;
				fb = 0;
				if (init) 
				{
					init = false;
				}
			}
		}
		if (Input.GetKey ("f")) 
		{
			if (fb == 0) 
			{
				lr = 0;
				ud = 0;
				fb = 1;
				if (init) 
				{
					init = false;
				}
			}
		}
		if (Input.GetKey ("b")) 
		{
			if (fb == 0) 
			{
				lr = 0;
				ud = 0;
				fb = -1;
				if (init) 
				{
					init = false;
				}
			}
		}
		if (gestureListener) 
		{
			if (gestureListener.IsSwipeLeft ()) 
			{
				if (lr == 0) 
				{
					lr = 1;
					ud = 0;
					fb = 0;
					if (init) 
					{
						init = false;
					}
				}
			}
			if (gestureListener.IsSwipeRight () && !init) 
			{
				if (lr == 0) 
				{
					lr = -1;
					ud = 0;
					fb = 0;
				}
			}
			if (gestureListener.IsSwipeUp ()) 
			{
				if (ud == 0) 
				{
					lr = 0;
					ud = 1;
					fb = 0;
					if (init) 
					{
						init = false;
					}
				}
			}
			if (gestureListener.IsSwipeDown ()) 
			{
				if (ud == 0) 
				{
					lr = 0;
					ud = -1;
					fb = 0;
					if (init) 
					{
						init = false;
					}
				}
			}
			if (gestureListener.IsPull ()) 
			{
				if (fb == 0) 
				{
					lr = 0;
					ud = 0;
					fb = 1;
					if (init) 
					{
						init = false;
					}
				}
			}
			if (gestureListener.IsPush ()) 
			{
				if (fb == 0) 
				{
					lr = 0;
					ud = 0;
					fb = -1;
					if (init) 
					{
						init = false;
					}
				}
			}
		}
	}
}
