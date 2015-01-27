using UnityEngine;
using System.Collections;

public class LevelButtons : MonoBehaviour {

	public string levelToLoad;
	public int levelNumber;
	//public string newLevel;
	//Use this for initialization
	void Start () {
		this.guiTexture.texture = (Texture)Resources.Load("Button");
	}

	void OnMouseEnter(){
		this.guiTexture.texture = (Texture)Resources.Load("Buttonenter");
	}

	void OnMouseExit(){
		this.guiTexture.texture = (Texture)Resources.Load("Button");
	}

	void OnMouseDown(){
		if(levelToLoad.Equals ("exit")){
			//exit game
			//print("exit");
			Application.Quit();
		}else
			if(levelToLoad.Equals("Next level"))
		{
			levelToLoad = GameObject.Find ("_GameManager_").GetComponent<GameManager>().NextLevel();
		}
		GameObject.Find ("_GameManager_").GetComponent<GameManager> ().SetLevel (levelNumber);
		Application.LoadLevel(levelToLoad);
	}
}
