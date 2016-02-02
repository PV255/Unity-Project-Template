using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {
    public GameObject canvasMain;
    public GameObject canvasPause;
    public GameObject canvasCredits;
    public GameObject canvasHUD;
    public GameObject canvasLevels;
    
    void Start(){

    }

    Vector3 camPos = new Vector3(0, 0, 0);
    Vector3 camAxY = new Vector3(0, 1, 0);
    bool gamePaused = false;

    Vector3 playerRestorePos;
    bool playerRestoreFreeze;

    void Update(){
        bool isInMainMenu = Application.loadedLevelName.Contains("mainMenu");
        
        if (isInMainMenu && Camera.main != null){
            Camera.main.transform.RotateAround(camPos, camAxY, Time.deltaTime * 2f);
        }

        if (!isInMainMenu && Input.GetKeyDown(KeyCode.Escape)){
            if (gamePaused){
                unpauseGame();
            }else{
                pauseGame();
            }
        }
    }

    public void pauseGame() {
        Time.timeScale = 0;
        canvasPause.SetActive(true);

        PlayerController ply = FindObjectOfType<PlayerController>();
        playerRestoreFreeze = ply.setIsPositionFixed(true);
        playerRestorePos = ply.gameObject.transform.position;

        gamePaused = true;
    }

    public void unpauseGame(){
        canvasPause.SetActive(false);
        Time.timeScale = 1;

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.transform.position = playerRestorePos;
        ply.setIsPositionFixed(playerRestoreFreeze);

        gamePaused = false;
    }

    public void newGame(){
        loadLevel("tutorialLevel");
    }

    public void loadLevel(string id){
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

        canvasHUD.SetActive(true);
    }

    public void quitGame(){
        Application.Quit();
    }

    public void quitLevel(){
        Time.timeScale = 1;

        gamePaused = false;
        Application.LoadLevel("mainMenu");

        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasHUD.SetActive(false);

        canvasMain.SetActive(true);
    }
}
