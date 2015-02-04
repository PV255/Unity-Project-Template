using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	private int score;
	//private string levelToLoad;
	public int currentLevel;
	public bool death;
	public bool winning;
	public GameObject snakehead;
	public GameObject snakeprefab;
	public GameObject gridCube;

	private GestureListener gestureListener; //gesture listener for Kinect
	public GameObject food;
	public AudioClip eating;

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

		GameObject snake1 = (GameObject)Instantiate (snakehead, new Vector3(4, 4, 2), Quaternion.identity);
		snake1.name = "snake1";
		GameObject snake2 = (GameObject)Instantiate (snakeprefab, new Vector3(3, 4, 2), Quaternion.identity);
		snake2.name = "snake2";
		GameObject snake3 = (GameObject)Instantiate (snakeprefab, new Vector3(2, 4, 2), Quaternion.identity);
		snake3.name = "snake3";
	
		for (int i = 0; i < 10; i++) 
		{
			for (int j = 0; j < 10; j++) 
			{
				for (int k = 0; k < 10; k++) 
				{
					Instantiate(gridCube, new Vector3(i, j, k), Quaternion.identity);
					GameObject.Find("gridCube(Clone)").gameObject.name = "gridCell(" + i + ", " + j + ", " + k + ")";
				}
			}
		}
	}

	void Update()
	{
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
	public void SetLevel(int number)
	{
		currentLevel = number;
		}
	
}
