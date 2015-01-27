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

						print ("score screen");
						Application.LoadLevel ("Score");
						print ("score screen LOADED");
				}
	}
}
