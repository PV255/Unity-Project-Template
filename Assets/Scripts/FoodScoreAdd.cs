using UnityEngine;
using System.Collections;

public class FoodScoreAdd : MonoBehaviour {
	static GameObject ob;
	static GameManager sc;
	// Use this for initialization
	void Start () {
		ob = GameObject.Find("_GameManager_");
		sc = (GameManager)ob.GetComponent(typeof(GameManager));
	}
	
	// Update is called once per frame
	void OnDestroy () {
			sc.AddPoints ();
	}
}
