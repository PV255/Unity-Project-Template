using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    public Text scoreText;

    float loadMainMenuIn;

    // Use this for initialization
    void Start () {
        loadMainMenuIn = Time.time + 5;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > loadMainMenuIn) {
            Application.LoadLevel(0);
        }
}

    public void initGameOver(int score) {
        Debug.Log("initGameOver - score: " + score);
        scoreText.text = score.ToString();
    }
}
