using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private LevelManager levelManager;
    public Text scoreText;
    private int score;

    void Awake() {
        Instance = this;
        score = 0;
        SetScoreText();
        
    }

	// Use this for initialization
	void Start () {
        
        DontDestroyOnLoad(gameObject);
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

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("New level was loaded");
        setLevelManager(GameObject.Find("LevelManager").GetComponent<LevelManager>());
    }
}
