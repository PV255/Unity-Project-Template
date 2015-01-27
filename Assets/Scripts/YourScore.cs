using UnityEngine;
using System.Collections;

public class YourScore : MonoBehaviour {
	GUIStyle aFont;
	public int newScore;
	public bool enteringScore = false;
	public int level;
	// Use this for initialization
	void Start () {
		GameObject.Find ("_GameManager_").GetComponent<GameManager> ().Alive ();
		print ("won level - i am alive");
		aFont = new GUIStyle();
		aFont.fontSize = 56;
		aFont.alignment = TextAnchor.MiddleCenter;
		aFont.normal.textColor = Color.magenta;
		//
			level = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel;
			GameObject.Find ("_GameManager_").GetComponent<GameManager> ().TakeOffPointsAddedAfterSnakeDead (3+level/2);
			print ("Your score:");
			if(GameObject.Find("_GameManager_").GetComponent<GameManager>().LastScore() != 0){
				enteringScore = true;
				newScore = GameObject.Find ("_GameManager_").GetComponent<GameManager>().LastScore();
	

	}
	
	// Update is called once per frame
//	void Update () {
	
	}
}
