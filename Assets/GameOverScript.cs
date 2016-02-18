using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    public Text scoreText;
    
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void initGameOver(int score) {
        Debug.Log("initGameOver - score: " + score);
        scoreText.text = score.ToString();
    }
}
