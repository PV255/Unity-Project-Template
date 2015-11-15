using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private LevelManager levelManager;
    public Text scoreText;
    private int score;

    void Awake() {
        if (Instance)
        {
            foreach (Transform child in transform) {
                DestroyImmediate(child.gameObject);
            }
            DestroyImmediate(this);
        }
        else{
            Instance = this;
            score = 0;
            SetScoreText();
            DontDestroyOnLoad(gameObject);
        }
        setLevelManager(GameObject.Find("LevelManager").GetComponent<LevelManager>());
    }

	// Use this for initialization
	void Start () {
        
        SetScoreText();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setLevelManager(LevelManager m)
    {
        levelManager = m;
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
