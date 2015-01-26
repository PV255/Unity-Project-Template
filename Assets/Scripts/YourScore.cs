using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	GUIStyle aFont;
	public int newScore;
	public bool enteringScore = false;
	// Use this for initialization
	void Start () {
		aFont = new GUIStyle();
		aFont.fontSize = 56;
		aFont.alignment = TextAnchor.MiddleCenter;
		aFont.normal.textColor = Color.magenta;
		//

			print ("Your score:");
			if(GameObject.Find ("_GameManager_").GetComponent<GameManager>().LastScore() != 0){
				enteringScore = true;
				newScore = GameObject.Find ("_GameManager_").GetComponent<GameManager>().LastScore();

	}
	
	// Update is called once per frame
//	void Update () {
	
	}
}
