using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBehaviourScript : MonoBehaviour {
    public GameObject canvasMain;
    public GameObject canvasPause;
    public GameObject canvasCredits;
    public GameObject canvasHUD;
    public GameObject canvasLevels;
    public GameObject canvasGameOver;

    public Text textGameOverScore;

    void Start() {

    }

    Vector3 camPos = new Vector3(0, 0, 0);
    Vector3 camAxY = new Vector3(0, 1, 0);
    bool gamePaused = false;

    Vector3 playerRestorePos;
    bool playerRestoreFreeze;

    void Update() {
        bool isInMainMenu = Application.loadedLevelName.Contains("mainMenu");

        if (isInMainMenu && Camera.main != null) {
            Camera.main.transform.RotateAround(camPos, camAxY, Time.deltaTime * 2f);
        }

        if (!isInMainMenu && Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                unpauseGame();
            } else {
                pauseGame();
            }
        }
    }

    public void pauseGame() {
        Time.timeScale = 0;
        canvasPause.SetActive(true);

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gamePaused = true;
    }

    public void unpauseGame() {
        canvasPause.SetActive(false);
        Time.timeScale = 1;

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gamePaused = false;
    }

    public void newGame() {
        loadLevel("tutorialLevel");
    }

    public void loadLevel(string id) {
        if (id == null) {
            return;
        }

        gamePaused = false;
        Application.LoadLevel(id);

        GameManager.Instance.newGameStarted();

        canvasMain.SetActive(false);
        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasGameOver.SetActive(false);

    }

    public void quitGame() {
        Application.Quit();
    }

    public void quitLevel() {
        Time.timeScale = 1;

        gamePaused = false;
        Application.LoadLevel("mainMenu");

        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasHUD.SetActive(false);
        canvasGameOver.SetActive(false);

        canvasMain.SetActive(true);
    }

    public void showGameOver() {
        Time.timeScale = 1;

        gamePaused = false;
        Application.LoadLevel("mainMenu");

        canvasMain.SetActive(false);
        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasHUD.SetActive(false);
        
        textGameOverScore.text = "Your Score: " + GameManager.Instance.getScore();

        canvasGameOver.SetActive(true);
    }

    void OnLevelWasLoaded(int id) {
        if (id == 1) {
            return;
        }

        canvasHUD.SetActive(true);
    }
}
