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

		if (levelToLoad == "scene1" || levelToLoad == "scene2" || levelToLoad == "scene3" || levelToLoad == "scene4" || levelToLoad == "scene5" || levelToLoad == "scene6" || levelToLoad == "scene7" || levelToLoad == "scene8" || levelToLoad == "scene9" || levelToLoad == "scene10") {
			KinectManager manager = KinectManager.Instance;
			print(manager);
			if (manager!=null)
			{
			manager.Player1Gestures [2] = KinectGestures.Gestures.SwipeUp;
			manager.Player1Gestures [3] = KinectGestures.Gestures.SwipeDown;
			manager.Player1Gestures [4] = KinectGestures.Gestures.Pull;
			manager.Player1Gestures [5] = KinectGestures.Gestures.Push;
			manager.ControlMouseCursor = false;
			}
		}

		Application.LoadLevel(levelToLoad);
	}
}
