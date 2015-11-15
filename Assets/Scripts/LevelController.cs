using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToNextLevel() {
        Application.LoadLevel(nextLevel);
    }
}
