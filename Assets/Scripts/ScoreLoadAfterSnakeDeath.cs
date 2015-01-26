using UnityEngine;
using System.Collections;

public class ScoreLoadAfterSnakeDeath : MonoBehaviour {

	void OnDestroy(){
		//Time.timeScale = 0.1F;
		print ("spomalenie");
		GameObject.Find ("_GameManager_").GetComponent<GameManager> ().TakeOffPointsAddedAfterSnakeDead (3);
		//yield return new WaitForSeconds(2);



		Application.LoadLevel("Score");
	}
}
