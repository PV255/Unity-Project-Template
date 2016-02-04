using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBehaviourScript : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvasPause;
    public GameObject canvasCredits;
    public GameObject canvasHUD;
    public GameObject canvasLevels;
    public GameObject canvasGameOver;

    public RawImage fade;
    public Text textGameOverScore;

    void Start()
    {

    }

    Vector3 camPos = new Vector3(0, 0, 0);
    Vector3 camAxY = new Vector3(0, 1, 0);
    bool gamePaused = false;

    Vector3 playerRestorePos;
    bool playerRestoreFreeze;

    void Update()
    {
        string lvlName = Application.loadedLevelName;
        bool nothingToPause = lvlName.Contains("mainMenu") || lvlName.Contains("gameOver");

        if (nothingToPause && Camera.main != null)
        {
            Camera.main.transform.RotateAround(camPos, camAxY, Time.deltaTime * 2f);
        }

        if (!nothingToPause && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                unpauseGame();
            }
            else {
                pauseGame();
            }
        }

        Color col = fade.color;
        col.a = Mathf.Clamp01(col.a - Time.deltaTime);
        fade.color = col;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        canvasPause.SetActive(true);

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gamePaused = true;
    }

    public void unpauseGame()
    {
        canvasPause.SetActive(false);
        Time.timeScale = 1;

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gamePaused = false;
    }

    private string newGameLevelName = "tutorialLevel";

    public void newGame()
    {
        loadLevel(newGameLevelName);

        newGameLevelName = "hub";
    }

    public void hideCanvases()
    {
        canvasMain.SetActive(false);
        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasHUD.SetActive(false);
        canvasGameOver.SetActive(false);
    }

    public void loadLevel(string id)
    {
        if (id == null)
        {
            return;
        }

        gamePaused = false;
        Application.LoadLevel(id);

        hideCanvases();

        GameManager.Instance.newGameStarted();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void quitLevel()
    {
        Time.timeScale = 1;

        gamePaused = false;
        Application.LoadLevel("mainMenu");

        hideCanvases();
    }

    public void showGameOver()
    {
        Time.timeScale = 1;

        gamePaused = false;
        Application.LoadLevel("gameOver");

        hideCanvases();

        textGameOverScore.text = "Your Score: " + GameManager.Instance.getScore();
    }

    void OnLevelWasLoaded(int id)
    {
        string lvlName = Application.loadedLevelName;

        if (lvlName.Contains("gameOver"))
        {
            canvasGameOver.SetActive(true);
            return;
        }

        if (lvlName.Contains("mainMenu"))
        {
            canvasMain.SetActive(true);
            return;
        }

        Color col = fade.color;
        col.a = 1;
        fade.color = col;

        canvasHUD.SetActive(true);
    }
}
