using UnityEngine;
using System.Collections;

public class Buttons2 : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/5 - 50, 200, 100), "Start"))
			Application.LoadLevel("scene1");
		if (GUI.Button(new Rect(Screen.width/2 - 100, (Screen.height/5)*2 -50, 200, 100), "Options"))
			print("You clicked the button!");
		if (GUI.Button(new Rect(Screen.width/2 - 100, (Screen.height/5)*3 -50, 200, 100), "Credits"))
			print("You clicked the button!");
		if (GUI.Button(new Rect(Screen.width/2 - 100, (Screen.height/5)*4 -50, 200, 100), "Exit"))
			print("EXIT");
	}
}
