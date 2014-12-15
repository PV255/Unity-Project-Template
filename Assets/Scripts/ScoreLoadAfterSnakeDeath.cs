using UnityEngine;
using System.Collections;

public class ScoreLoadAfterSnakeDeath : MonoBehaviour {

	void OnDestroy(){
		GameObject.Find ("_GameManager_").GetComponent<GameManager> ().TakeOffPointsAddedAfterSnakeDead (3);
		Application.LoadLevel("Score");
	}
}
