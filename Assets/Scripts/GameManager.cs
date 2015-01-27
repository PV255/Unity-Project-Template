using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private int score;
	//private string levelToLoad;
	public int currentLevel;
	public bool death;
	public bool winning;
	void Start()
	{
		death = false;
		winning = false;
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
		print("erasing " + i + "points");
	}
	public string NextLevel()
	{
		currentLevel++;
		return "scene" + currentLevel;
		}
	public bool Alive()
	{
		death = false;
		return death;
	}
	public bool Dead()
	{
		death = true;
		return death;
	}
	public void Winning()
	{
		winning = true;
	}
	public void NotWinning()
	{
		winning = false;
	}
	/*public bool AmIDead()
	{
		return death;
	}*/
	
}
