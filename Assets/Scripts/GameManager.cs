using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public LevelManager levelManager;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToNextLevel() {
        levelManager.GetComponent<LevelManager>().GoToNextLevel();
    }
}
