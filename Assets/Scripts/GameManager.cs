using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private int score;
	//private string levelToLoad;
	public int currentLevel;

	void Start()
	{
		score = 0;
		//This keeps the object alive across multiple scenes.
		DontDestroyOnLoad (this.gameObject);
		
		//Sets the position of GUI Text
		this.guiText.pixelOffset = new Vector2 (Screen.width/6,Screen.height/2);
		this.guiText.text = "Score:" + score;
		
		//Checks, if there is no other GameManager in scene
		if(GameObject.Find("_GameManager_") != this.gameObject)
			Destroy(this.gameObject);
	}
	void Update(){
		this.guiText.text = "Score:" + score;
	}

	public void AddPoints(){
		score++;
//		print (score);
	}

	public int LastScore(){
		return score;
	}
	public void EraseScore(){
		score = 0;
	}

	public void TakeOffPointsAddedAfterSnakeDead(int i){
		score = score - i;
	}
	public string NextLevel()
	{
		currentLevel++;
		return "scene" + currentLevel;
		}
	
}
