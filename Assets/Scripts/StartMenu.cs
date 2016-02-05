using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Canvas quitDialog;
    public Canvas aboutCanvas;
    public Canvas controlsCanvas;
    public Canvas controls1Canvas;
    public Canvas optionsCanvas;
    public Canvas menuCanvas;
    public Canvas uiCanvas;
    public Button playButton;
    public Button exitButton;
    public Button controlsButton;
    public Button optionsButton;
    public Button aboutButton;
    public AudioSource audioSource;
    public Toggle musicToggle;

    public GameObject snakeObject;
    public Snake snakeScript;
    public GameObject backgroundObject;
    public AddPortal addPortalScript;

    // Use this for initialization
    void Start () {
        
        quitDialog = quitDialog.GetComponent<Canvas>(); //assigned actual game object to variable
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
        controlsCanvas = controlsCanvas.GetComponent<Canvas>();
        controls1Canvas = controls1Canvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        menuCanvas = menuCanvas.GetComponent<Canvas>();
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        aboutButton = aboutButton.GetComponent<Button>();
        audioSource = audioSource.GetComponent<AudioSource>();
        musicToggle = musicToggle.GetComponent<Toggle>();

        snakeObject = GameObject.FindGameObjectWithTag("Snake");
        snakeScript = snakeObject.GetComponent<Snake>();
        backgroundObject = GameObject.FindGameObjectWithTag("Background");
        addPortalScript = backgroundObject.GetComponent<AddPortal>();

        audioSource.mute = true;
        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
        controlsCanvas.enabled = false;
        controls1Canvas.enabled = false;
        optionsCanvas.enabled = false;
        uiCanvas.enabled = false;
    }

    public void ControlsPress()
    {
        controls1Canvas.enabled = true;
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
        MenuButtonsEnamble(false);
    }
	
	// EXIT
	public void ExitPress()
    {
        quitDialog.enabled = true;
        MenuButtonsEnamble(false);
    }

    private void MenuButtonsEnamble(bool enable)
    {
        playButton.enabled = enable;
        exitButton.enabled = enable;
        controlsButton.enabled = enable;
        optionsButton.enabled = enable;
        aboutButton.enabled = enable;
    }

    public void OkSoundDialog()
    {
        optionsCanvas.enabled = false;
        if (musicToggle.isOn)
            audioSource.mute = false;
        else audioSource.mute = true;
        MenuButtonsEnamble(true);
    }

    public void Back()
    {
        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
        controlsCanvas.enabled = false;
        optionsCanvas.enabled = false;

        MenuButtonsEnamble(true);
    }

    public void NextControls()
    {
        controls1Canvas.enabled = false;
        controlsCanvas.enabled = true;
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
