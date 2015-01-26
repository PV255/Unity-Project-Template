using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public string levelToLoad;
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
		Application.LoadLevel(levelToLoad);
	}
}
