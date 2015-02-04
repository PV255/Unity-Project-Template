using UnityEngine;
using System.Collections;

public class ScoreLoadAfterSnakeDeath : MonoBehaviour {

	public int level;
	public bool death;

	void OnDestroy(){
		death = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().death;
		level = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel;
		print("checking death: " + death);
		if (death) {
						KinectManager manager = KinectManager.Instance;
						manager.Player1Gestures [2] = KinectGestures.Gestures.RightHandCursor;
						manager.Player1Gestures [3] = KinectGestures.Gestures.LeftHandCursor;
						manager.Player1Gestures [4] = KinectGestures.Gestures.Click;
						manager.Player1Gestures [5] = KinectGestures.Gestures.None;
						manager.ControlMouseCursor = true;

						print ("score screen");
						Application.LoadLevel ("Score");
						print ("score screen LOADED");
				}


	}
}
