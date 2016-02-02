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

    Vector3 pos = new Vector3(0, 0, 0);
    Vector3 y = new Vector3(0, 1, 0);
    Vector3 playerRestorePos;
    bool gamePaused = false;

    void Update(){
        bool isInMainMenu = Application.loadedLevelName.Contains("mainMenu");
        
        if (isInMainMenu && Camera.main != null){
            Camera.main.transform.RotateAround(pos, y, Time.deltaTime * 2f);
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
        ply.setIsPositionFixed(true);
        playerRestorePos = ply.gameObject.transform.position;

        gamePaused = true;
    }

    public void unpauseGame(){
        canvasPause.SetActive(false);
        Time.timeScale = 1;

        PlayerController ply = FindObjectOfType<PlayerController>();
        ply.gameObject.transform.position = playerRestorePos;
        ply.setIsPositionFixed(false);

        gamePaused = false;
    }

    public void newGame(){
        loadLevel("tutorialLevel");
    }

    public void loadLevel(string id){
        if (id == null) {
            return;
        }

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

        Application.LoadLevel("mainMenu");

        canvasMain.SetActive(true);

        canvasPause.SetActive(false);
        canvasCredits.SetActive(false);
        canvasLevels.SetActive(false);
        canvasHUD.SetActive(false);
    }
}
