using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text; 

public class AvatarController : MonoBehaviour
{	
	// Bool that determines whether the avatar is active.
	//public bool Active = true;
	
	// Bool that has the characters (facing the player) actions become mirrored. Default false.
	public bool MirroredMovement = false;
	
	// Bool that determines whether the avatar will move or not in space.
	//public bool MovesInSpace = true;
	
	// Bool that determines whether the avatar is allowed to jump -- vertical movement
	// can cause some models to behave strangely, so use at your own discretion.
	public bool VerticalMovement = false;
	
	// Rate at which avatar will move through the scene. The rate multiplies the movement speed (.001f, i.e dividing by 1000, unity's framerate).
	public int MoveRate = 1;
	
	// Slerp smooth factor
	public float SmoothFactor = 10.0f;
	
	
	// Public variables that will get matched to bones. If empty, the kinect will simply not track it.
	// These bones can be set within the Unity interface.
	public Transform Hips;
	public Transform Spine;
	public Transform Neck;
	public Transform Head;
	
	public Transform LeftShoulder;
	public Transform LeftUpperArm;
	public Transform LeftElbow; 
	//public Transform LeftWrist;
	public Transform LeftHand;
	//public Transform LeftFingers;
	
	public Transform RightShoulder;
	public Transform RightUpperArm;
	public Transform RightElbow;
	//public Transform RightWrist;
	public Transform RightHand;
	//public Transform RightFingers;
	
	public Transform LeftThigh;
	public Transform LeftKnee;
	public Transform LeftFoot;
	public Transform LeftToes;
	
	public Transform RightThigh;
	public Transform RightKnee;
	public Transform RightFoot;
	public Transform RightToes;
	
	public Transform Root;
	
	// A required variable if you want to rotate the model in space.
	public GameObject offsetNode;
	
	// Variable to hold all them bones. It will initialize the same size as initialRotations.
	private Transform[] bones;
	
	// Rotations of the bones when the Kinect tracking starts.
    private Quaternion[] initialRotations;
	private Quaternion[] initialLocalRotations;
	
	// Rotations of the bones when the Kinect tracking starts.
    private Vector3[] initialDirections;
	
	// Calibration Offset Variables for Character Position.
	private bool OffsetCalibrated = false;
	private float XOffset, YOffset, ZOffset;
	private Quaternion originalRotation;
	
	// Lastly used Kinect UserID
	//private uint LastUserID;
	
	// private instance of the KinectManager
	private KinectManager kinectManager;

	private Vector3 hipsUp;  // up vector of the hips
	private Vector3 hipsRight;  // right vector of the hips
	private Vector3 chestRight;  // right vectory of the chest
	
	
    public void Start()
    {	
		//GestureInfo = GameObject.Find("GestureInfo");
		//HandCursor = GameObject.Find("HandCursor");
		
		// Holds our bones for later.
		bones = new Transform[25];
		
		// Initial rotations and directions of the bones.
		initialRotations = new Quaternion[bones.Length];
		initialLocalRotations = new Quaternion[bones.Length];
		initialDirections = new Vector3[bones.Length];
		
		// Map bones to the points the Kinect tracks
		MapBones();

		// Get initial bone directions
		GetInitialDirections();
		
		// Get initial bone rotations
		GetInitialRotations();
		
		// Set the model to the calibration pose
        RotateToCalibrationPose(0, KinectManager.IsCalibrationNeeded());
	}
	
	// Update the avatar each frame.
    public void UpdateAvatar(uint UserID)
    {	
		//LastUserID = UserID;
		bool flipJoint = !MirroredMovement;
		
		// Get the KinectManager instance
		if(kinectManager == null)
		{
			kinectManager = KinectManager.Instance;
		}
		
		// Update Head, Neck, Spine, and Hips normally.
		TransformBone(UserID, KinectWrapper.SkeletonJoint.HIPS, 1, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.SPINE, 2, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.NECK, 3, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.HEAD, 4, flipJoint);
		
		// Beyond this, switch the arms and legs.
		
		// Left Arm --> Right Arm
		TransformSpecialBone(UserID, KinectWrapper.SkeletonJoint.LEFT_COLLAR, !MirroredMovement ? 5 : 11, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_SHOULDER, !MirroredMovement ? 6 : 12, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_ELBOW, !MirroredMovement ? 7 : 13, flipJoint);
		//TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_WRIST, !MirroredMovement ? 8 : 14, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_HAND, !MirroredMovement ? 8 : 14, flipJoint);
		//TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_FINGERTIP, !MirroredMovement ? 10 : 16, flipJoint);
		
		// Right Arm --> Left Arm
		TransformSpecialBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_COLLAR, !MirroredMovement ? 11 : 5, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_SHOULDER, !MirroredMovement ? 12 : 6, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_ELBOW, !MirroredMovement ? 13 : 7, flipJoint);
		//TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_WRIST, !MirroredMovement ? 14 : 8, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_HAND, !MirroredMovement ? 14 : 8, flipJoint);
		TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_FINGERTIP, !MirroredMovement ? 16 : 10, flipJoint);
		
		// Hips 2
		TransformSpecialBone(UserID, KinectWrapper.SkeletonJoint.HIPS, 16, flipJoint);
		
		//if(!IsNearMode)
		{
			// Left Leg --> Right Leg
			TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_HIP, !MirroredMovement ? 17 : 21, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_KNEE, !MirroredMovement ? 18 : 22, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_ANKLE, !MirroredMovement ? 19 : 23, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.LEFT_FOOT, !MirroredMovement ? 20 : 24, flipJoint);
			
			// Right Leg --> Left Leg
			TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_HIP, !MirroredMovement ? 21 : 17, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_KNEE, !MirroredMovement ? 22 : 18, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_ANKLE, !MirroredMovement ? 23 : 19, flipJoint);
			TransformBone(UserID, KinectWrapper.SkeletonJoint.RIGHT_FOOT, !MirroredMovement ? 24 : 20, flipJoint);	
		}
		
		// If the avatar is supposed to move in the space, move it.
		//if (MovesInSpace)
		{
			MoveAvatar(UserID);
		}
    }
	
	// Calibration pose is simply initial position with hands raised up. Rotation must be 0,0,0 to calibrate.
    public void RotateToCalibrationPose(uint userId, bool needCalibration)
    {	
		// Reset the rest of the model to the original position.
        RotateToInitialPosition();
		
		if(needCalibration)
		{
			if(offsetNode != null)
			{
				// Set the offset's rotation to 0.
				offsetNode.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
			
//			// Right Elbow
//			if(RightElbow != null)
//	        	RightElbow.rotation = Quaternion.Euler(0, -90, 90) * 
//					initialRotations[(int)KinectWrapper.SkeletonJoint.RIGHT_ELBOW];
//			
//			// Left Elbow
//			if(LeftElbow != null)
//	        	LeftElbow.rotation = Quaternion.Euler(0, 90, -90) * 
//					initialRotations[(int)KinectWrapper.SkeletonJoint.LEFT_ELBOW];

			if(offsetNode != null)
			{
				// Restore the offset's rotation
				offsetNode.transform.rotation = originalRotation;
			}
		}
    }
	
	// Invoked on the successful calibration of a player.
	public void SuccessfulCalibration(uint userId)
	{
		// reset the models position
		if(offsetNode != null)
		{
			offsetNode.transform.rotation = originalRotation;
		}
		
		// re-calibrate the position offset
		OffsetCalibrated = false;
	}
	
	// Apply the rotations tracked by kinect to the joints.
    void TransformBone(uint userId, KinectWrapper.SkeletonJoint joint, int boneIndex, bool flip)
    {
		Transform boneTransform = bones[boneIndex];
		if(boneTransform == null || kinectManager == null)
			return;
		
		int iJoint = (int)joint;
		if(iJoint < 0)
			return;
		
		// Get Kinect joint orientation
		Quaternion jointRotation = kinectManager.GetJointOrientation(userId, iJoint, flip);
		if(jointRotation == Quaternion.identity)
			return;
		
		// Smoothly transition to the new rotation
		Quaternion newRotation = Kinect2AvatarRot(jointRotation, boneIndex);
		
		if(SmoothFactor != 0f)
        	boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, newRotation, SmoothFactor * Time.deltaTime);
		else
			boneTransform.rotation = newRotation;
	}
	
	// Apply the rotations tracked by kinect to a special joint
	void TransformSpecialBone(uint userId, KinectWrapper.SkeletonJoint joint, int boneIndex, bool flip)
    {
		Transform boneTransform = bones[boneIndex];
		if(boneTransform == null || kinectManager == null)
			return;
		
		// Get initial bone direction
		Vector3 initialDir = initialDirections[boneIndex];
		if(initialDir == Vector3.zero)
			return;
		
		int iJoint = (int)joint;
//		if(iJoint < 0)
//			return;
		
		// Get Kinect joint orientation
		Vector3 jointDir = Vector3.zero;
		
		if(boneIndex == 16)  // Hip_center
		{
			initialDir = hipsUp;
			
			// target is a vector from hip_center to average of hips left and right
			jointDir = ((kinectManager.GetJointPosition(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.HipLeft) + 
				kinectManager.GetJointPosition(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.HipRight)) / 2.0f) - 
				kinectManager.GetJointPosition(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.HipCenter);

			jointDir.z = -jointDir.z;
			if(!flip)
				jointDir.x = -jointDir.x;
		}
		else if(boneIndex == 5 || boneIndex == 11)  // Clavicle_left or Clavicle_right
		{
			// target is a vector from shoulder_center to bone
			jointDir = kinectManager.GetDirectionBetweenJoints(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter, iJoint, !flip, true);
		}
		else
		{
			jointDir = kinectManager.GetDirectionBetweenJoints(userId, iJoint - 1, iJoint, !flip, true);
		}

		if(jointDir == Vector3.zero)
			return;
		
//		Vector3 upDir = Vector3.zero;
//		Vector3 rightDir = Vector3.zero;
//		
//		if(joint == KinectWrapper.SkeletonJoint.SPINE)
//		{
////			// the spine in the Kinect data is ~40 degrees back from the hip
////			jointDir = Quaternion.Euler(40, 0, 0) * jointDir;
//
//			if(Hips && LeftThigh && RightThigh)
//			{
//				upDir = ((LeftThigh.position + RightThigh.position) / 2.0f) - Hips.transform.position;
//				rightDir = RightThigh.transform.position - LeftThigh.transform.position;
//			}
//		}
		
		boneTransform.localRotation = initialLocalRotations[boneIndex];
		
		// transform it into bone-local space
		jointDir = transform.TransformDirection(jointDir);
		jointDir = boneTransform.InverseTransformDirection(jointDir);
		
		//create a rotation that rotates dir into target
		Quaternion quat = Quaternion.FromToRotation(initialDir, jointDir);
		
		//if bone is the hip override, add in the rotation along the hips
		if(boneIndex == 16)
		{
			//rotate the hips so they face forward (determined by the hips)
			initialDir = hipsRight;
			jointDir = kinectManager.GetDirectionBetweenJoints(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.HipLeft, (int)KinectWrapper.NuiSkeletonPositionIndex.HipRight, false, flip);
			
			if(jointDir != Vector3.zero)
			{
				jointDir = transform.TransformDirection(jointDir);
				jointDir = boneTransform.InverseTransformDirection(jointDir);
				jointDir -= Vector3.Project(jointDir, initialDirections[boneIndex]);
				
				quat *= Quaternion.FromToRotation(initialDir, jointDir);
			}
		}
//		//if bone is the spine, add in the rotation along the spine
//		else if(joint == KinectWrapper.SkeletonJoint.SPINE)
//		{
//			//rotate the chest so that it faces forward (determined by the shoulders)
//			initialDir = chestRight;
//			jointDir = kinectManager.GetDirectionBetweenJoints(userId, (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft, (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight, false, flip);
//			
//			if(jointDir != Vector3.zero)
//			{
//				jointDir = transform.TransformDirection(jointDir);
//				jointDir = boneTransform.InverseTransformDirection(jointDir);
//				jointDir -= Vector3.Project(jointDir, initialDirections[boneIndex]);
//				
//				quat *= Quaternion.FromToRotation(initialDir, jointDir);
//			}
//		}
		
		boneTransform.localRotation *= quat;
		
//		if(joint == KinectWrapper.SkeletonJoint.SPINE && upDir != Vector3.zero && rightDir != Vector3.zero)
//		{
//			RestoreBone(bones[1], hipsUp, upDir);
//			RestoreBone(bones[1], hipsRight, rightDir);
//		}
		
	}
	
	// Transform targetDir into bone-local space (independant of the transform of the controller)
	void RestoreBone(Transform boneTransform, Vector3 initialDir, Vector3 targetDir)
	{
		//targetDir = transform.TransformDirection(targetDir);
		targetDir = boneTransform.InverseTransformDirection(targetDir);
		
		//create a rotation that rotates dir into target
		Quaternion quat = Quaternion.FromToRotation(initialDir, targetDir);
		boneTransform.localRotation *= quat;
	}
	
	// Moves the avatar in 3D space - pulls the tracked position of the spine and applies it to root.
	// Only pulls positional, not rotational.
	void MoveAvatar(uint UserID)
	{
		if(Root == null || kinectManager == null)
			return;
		if(!kinectManager.IsJointTracked(UserID, (int)KinectWrapper.SkeletonJoint.HIPS))
			return;
		
        // Get the position of the body and store it.
		Vector3 trans = kinectManager.GetUserPosition(UserID);
		
		// If this is the first time we're moving the avatar, set the offset. Otherwise ignore it.
		if (!OffsetCalibrated)
		{
			OffsetCalibrated = true;
			
			XOffset = !MirroredMovement ? trans.x * MoveRate : -trans.x * MoveRate;
			YOffset = trans.y * MoveRate;
			ZOffset = -trans.z * MoveRate;
		}
	
		// Smoothly transition to the new position
		Vector3 targetPos = Kinect2AvatarPos(trans, VerticalMovement);
		Root.localPosition = Vector3.Lerp(Root.localPosition, targetPos, SmoothFactor * Time.deltaTime);
	}
	
	// If the bones to be mapped have been declared, map that bone to the model.
	void MapBones()
	{
		// If they're not empty, pull in the values from Unity and assign them to the array.
		if(Hips != null)
			bones[1] = Hips;
		if(Spine != null)
			bones[2] = Spine;
		if(Neck != null)
			bones[3] = Neck;
		if(Head != null)
			bones[4] = Head;
		
		if(LeftShoulder != null)
			bones[5] = LeftShoulder;
		if(LeftUpperArm != null)
			bones[6] = LeftUpperArm;
		if(LeftElbow != null)
			bones[7] = LeftElbow;
//		if(LeftWrist != null)
//			bones[8] = LeftWrist;
		if(LeftHand != null)
			bones[8] = LeftHand;
//		if(LeftFingers != null)
//			bones[10] = LeftFingers;
		
		if(RightShoulder != null)
			bones[11] = RightShoulder;
		if(RightUpperArm != null)
			bones[12] = RightUpperArm;
		if(RightElbow != null)
			bones[13] = RightElbow;
//		if(RightWrist != null)
//			bones[14] = RightWrist;
		if(RightHand != null)
			bones[14] = RightHand;
//		if(RightFingers != null)
//			bones[16] = RightFingers;
		
		// Hips 2
		if(Hips != null)
			bones[16] = Hips;
		
		if(LeftThigh != null)
			bones[17] = LeftThigh;
		if(LeftKnee != null)
			bones[18] = LeftKnee;
		if(LeftFoot != null)
			bones[19] = LeftFoot;
		if(LeftToes != null)
			bones[20] = LeftToes;
		
		if(RightThigh != null)
			bones[21] = RightThigh;
		if(RightKnee != null)
			bones[22] = RightKnee;
		if(RightFoot != null)
			bones[23] = RightFoot;
		if(RightToes!= null)
			bones[24] = RightToes;
	}
	
	// Capture the initial directions of the bones
	void GetInitialDirections()
	{
		int[] intermediateBone = { 1, 2, 3, 5, 6, 7, 11, 12, 13, 17, 18, 19, 21, 22, 23};
		
		for (int i = 0; i < bones.Length; i++)
		{
			if(Array.IndexOf(intermediateBone, i) >= 0)
			{
				// intermediary joint
				if(bones[i] && bones[i + 1])
				{
					initialDirections[i] = bones[i + 1].position - bones[i].position;
					initialDirections[i] = bones[i].InverseTransformDirection(initialDirections[i]);
				}
				else
				{
					initialDirections[i] = Vector3.zero;
				}
			}
//			else if(i == 16)
//			{
//				// Hips 2
//				initialDirections[i] = Vector3.zero;
//			}
			else
			{
				// end joint
				initialDirections[i] = Vector3.zero;
			}
		}
		
		// special directions
		if(Hips && LeftThigh && RightThigh)
		{
			hipsUp = ((RightThigh.position + LeftThigh.position) / 2.0f) - Hips.position;
			hipsUp = Hips.InverseTransformDirection(hipsUp);
			
			hipsRight = RightThigh.position - LeftThigh.position;
			hipsRight = Hips.InverseTransformDirection(hipsRight);
			
			// make hipRight orthogonal to the direction of the hips
			Vector3.OrthoNormalize(ref hipsUp, ref hipsRight);
			initialDirections[16] = hipsUp;
		}
		
		if(Spine && LeftUpperArm && RightUpperArm)
		{
			chestRight = RightUpperArm.position - LeftUpperArm.position;
			chestRight = Spine.InverseTransformDirection(chestRight);
			
			// make chestRight orthogonal to the direction of the spine
			chestRight -= Vector3.Project(chestRight, initialDirections[2]);
		}
	}

	// Capture the initial rotations of the bones
	void GetInitialRotations()
	{
		if(offsetNode != null)
		{
			// Store the original offset's rotation.
			originalRotation = offsetNode.transform.rotation;
			// Set the offset's rotation to 0.
			offsetNode.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
		
		for (int i = 0; i < bones.Length; i++)
		{
			if (bones[i] != null)
			{
				initialRotations[i] = bones[i].rotation;
				initialLocalRotations[i] = bones[i].localRotation;
			}
		}

		if(offsetNode != null)
		{
			// Restore the offset's rotation
			offsetNode.transform.rotation = originalRotation;
		}
	}
	
	// Set bones to initial position.
    public void RotateToInitialPosition()
    {	
		if(bones == null)
			return;
		
		if(offsetNode != null)
		{
			// Set the offset's rotation to 0.
			offsetNode.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
		
		// For each bone that was defined, reset to initial position.
		for (int i = 0; i < bones.Length; i++)
		{
			if (bones[i] != null)
			{
				bones[i].rotation = initialRotations[i];
			}
		}

		if(Root != null)
		{
			Root.localPosition = Vector3.zero;
		}

		if(offsetNode != null)
		{
			// Restore the offset's rotation
			offsetNode.transform.rotation = originalRotation;
		}
    }
	
	// Converts kinect joint rotation to avatar joint rotation, depending on joint initial rotation and offset rotation
	Quaternion Kinect2AvatarRot(Quaternion jointRotation, int boneIndex)
	{
		// Apply the new rotation.
        Quaternion newRotation = jointRotation * initialRotations[boneIndex];
		
		//If an offset node is specified, combine the transform with its
		//orientation to essentially make the skeleton relative to the node
		if (offsetNode != null)
		{
			// Grab the total rotation by adding the Euler and offset's Euler.
			Vector3 totalRotation = newRotation.eulerAngles + offsetNode.transform.rotation.eulerAngles;
			// Grab our new rotation.
			newRotation = Quaternion.Euler(totalRotation);
		}
		
		return newRotation;
	}
	
	// Converts Kinect position to avatar skeleton position, depending on initial position, mirroring and move rate
	Vector3 Kinect2AvatarPos(Vector3 jointPosition, bool bMoveVertically)
	{
		float xPos;
		float yPos;
		float zPos;
		
		// If movement is mirrored, reverse it.
		if(!MirroredMovement)
			xPos = jointPosition.x * MoveRate - XOffset;
		else
			xPos = -jointPosition.x * MoveRate - XOffset;
		
		yPos = jointPosition.y * MoveRate - YOffset;
		zPos = -jointPosition.z * MoveRate - ZOffset;
		
		// If we are tracking vertical movement, update the y. Otherwise leave it alone.
		Vector3 avatarJointPos = new Vector3(xPos, bMoveVertically ? yPos : 0f, zPos);
		
		return avatarJointPos;
	}
	
	// converts Kinect joint position to position relative to the user position (Hips)
	Vector3 Kinect2AvatarRelPos(Vector3 jointPosition, Vector3 userPosition)
	{
		jointPosition.z = !MirroredMovement ? -jointPosition.z : jointPosition.z;
		
		jointPosition -= userPosition;
		jointPosition.z = -jointPosition.z;
		
//		if(MirroredMovement)
//		{
//			jointPosition.x = -jointPosition.x;
//		}
		
		return jointPosition;
	}
	
}

