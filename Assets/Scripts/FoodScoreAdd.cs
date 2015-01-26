using UnityEngine;
using System.Collections;

public class FoodScoreAdd : MonoBehaviour {
	static GameObject ob;
	static GameManager sc;
	public int score;
	// Use this for initialization
	void Start () {
		ob = GameObject.Find("_GameManager_");
		sc = (GameManager)ob.GetComponent(typeof(GameManager));
	}
	
	// Update is called once per frame
	void OnDestroy () {
			sc.AddPoints ();
		print ("eaten food");
		score = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().LastScore();

		
		//uspesne ukonceny level - had zjedol 5 potrav
		if ((score%5) == 0)
		{
			print("won the level");
			Application.LoadLevel("WinningScreen");
			print("winning");
		}
	}
}
