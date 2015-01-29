using UnityEngine;
using System;
using System.Collections;

public class PointManController : MonoBehaviour 
{
	public bool MoveVertically = false;
	public bool MirroredMovement = false;

	//public GameObject debugText;
	
	public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Shoulder_Center;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	
	private GameObject[] _bones; 
	
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 initialPosOffset = Vector3.zero;
	private uint initialPosUserID = 0;
	
	
	void Start () 
	{
		//store bones in a list for easier access
		_bones = new GameObject[(int)KinectWrapper.NuiSkeletonPositionIndex.Count] {
			Hip_Center, Spine, Shoulder_Center, Head,
			Shoulder_Left, Elbow_Left, Wrist_Left, Hand_Left,
			Shoulder_Right, Elbow_Right, Wrist_Right, Hand_Right,
			Hip_Left, Knee_Left, Ankle_Left, Foot_Left,
			Hip_Right, Knee_Right, Ankle_Right, Foot_Right
		};
		
		// debug text
		//debugText = GameObject.Find("CalibrationText");
		
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// get 1st player
		uint playerID = KinectManager.Instance != null ? KinectManager.Instance.GetPlayer1ID() : 0;
		
		if(playerID <= 0)
		{
			// reset the pointman position and rotation
			if(transform.position != initialPosition)
				transform.position = initialPosition;
			
			if(transform.rotation != initialRotation)
				transform.rotation = initialRotation;
			
			return;
		}
		
		// set the position in space
		Vector3 posPointMan = KinectManager.Instance.GetUserPosition(playerID);
		posPointMan.z = !MirroredMovement ? -posPointMan.z : posPointMan.z;
		
		// store the initial position
		if(initialPosUserID != playerID)
		{
			initialPosUserID = playerID;
			initialPosOffset = transform.position - (MoveVertically ? posPointMan : new Vector3(posPointMan.x, 0, posPointMan.z));
		}
		
		transform.position = initialPosOffset + (MoveVertically ? posPointMan : new Vector3(posPointMan.x, 0, posPointMan.z));
		
		// update the local positions of the bones
		int jointsCount = (int)KinectWrapper.NuiSkeletonPositionIndex.Count;
		
		for(int i = 0; i < jointsCount; i++) 
		{
			if(_bones[i] != null)
			{
				if(KinectManager.Instance.IsJointTracked(playerID, i))
				{
					_bones[i].gameObject.SetActive(true);
					
					int joint = MirroredMovement ? KinectWrapper.GetSkeletonMirroredJoint(i): i;
					Vector3 posJoint = KinectManager.Instance.GetJointPosition(playerID, joint);
					posJoint.z = !MirroredMovement ? -posJoint.z : posJoint.z;
					Quaternion rotJoint = KinectManager.Instance.GetJointOrientation(playerID, joint, !MirroredMovement);
					
					posJoint -= posPointMan;
					posJoint.z = -posJoint.z;
					
					if(MirroredMovement)
					{
						posJoint.x = -posJoint.x;
					}

					_bones[i].transform.localPosition = posJoint;
					_bones[i].transform.localRotation = rotJoint;
				}
				else
				{
					_bones[i].gameObject.SetActive(false);
				}
			}	
		}
	}

}
