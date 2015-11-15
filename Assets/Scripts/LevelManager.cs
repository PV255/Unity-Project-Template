using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    private GameManager gameManager;
    public string nextLevel;

	// Use this for initialization
	void Start () {
        gameManager = GameManager.Instance;
        gameManager.setLevelManager(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToNextLevel() {
        Application.LoadLevel(nextLevel);
    }
}
