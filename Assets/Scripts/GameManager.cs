using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private GameObject levelManager;
    public Text scoreText;
    private int score;

    void Awake() {
        Instance = this;
        score = 0;
        SetScoreText();
    }

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("LevelManager");
        DontDestroyOnLoad(gameObject);
        
        SetScoreText();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToNextLevel() {
        levelManager.GetComponent<LevelManager>().GoToNextLevel();
    }

    public void AddScore(int ammount)
    {
        score += ammount;
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
