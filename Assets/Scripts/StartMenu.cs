using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Canvas quitDialog;
    public Canvas aboutCanvas;
    public Canvas controlsCanvas;
    public Canvas optionsCanvas;
    public Canvas menuCanvas;
    public Canvas uiCanvas;
    public Button playButton;
    public Button exitButton;
    public Button controlsButton;
    public Button optionsButton;
    public Button aboutButton;

    public GameObject snakeObject;
    public Snake snakeScript;
    public GameObject backgroundObject;
    public AddPortal addPortalScript;

    // Use this for initialization
    void Start () {
        
        quitDialog = quitDialog.GetComponent<Canvas>(); //assigned actual game object to variable
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
        controlsCanvas = controlsCanvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        menuCanvas = menuCanvas.GetComponent<Canvas>();
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        aboutButton = aboutButton.GetComponent<Button>();

        snakeObject = GameObject.FindGameObjectWithTag("Snake");
        snakeScript = snakeObject.GetComponent<Snake>();
        backgroundObject = GameObject.FindGameObjectWithTag("Background");
        addPortalScript = backgroundObject.GetComponent<AddPortal>();


        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
        controlsCanvas.enabled = false;
        optionsCanvas.enabled = false;
        uiCanvas.enabled = false;
    }

    public void ControlsPress()
    {
        controlsCanvas.enabled = true;
    }

    // ABOUT
    public void AboutPress()
    {
        aboutCanvas.enabled = true;
        //disabling
    }

    // OPTIONS
    public void OptionsPress()
    {
        optionsCanvas.enabled = true;
        DisableMenuButtons();
    }
	
	// EXIT
	public void ExitPress()
    {
        quitDialog.enabled = true;
        DisableMenuButtons();
    }

    private void DisableMenuButtons()
    {
        playButton.enabled = false;
        exitButton.enabled = false;
        controlsButton.enabled = false;
        optionsButton.enabled = false;
        aboutButton.enabled = false;
    }

    public void Back()
    {
        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
        controlsCanvas.enabled = false;
        optionsCanvas.enabled = false;

        playButton.enabled = true;
        exitButton.enabled = true;
        controlsButton.enabled = true;
        optionsButton.enabled = true;
        aboutButton.enabled = true;
    }

    public void StartLevel()
    {
        menuCanvas.enabled = false;
        uiCanvas.enabled = true;
        snakeScript.setInMenu(false);
        snakeScript.setPause(snakeScript.isPaused());
        addPortalScript.setInMenu(false);
        addPortalScript.setPause(snakeScript.isPaused());
    }

    //Exit - yes press
    public void ExitGame()
    {
        Application.Quit();
    }
}
