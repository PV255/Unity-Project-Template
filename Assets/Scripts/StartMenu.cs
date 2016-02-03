using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Canvas quitDialog;
    public Canvas aboutCanvas;
    public Button playButton;
    public Button exitButton;
    public Button controlsButton;
    public Button optionsButton;
    public Button aboutButton;

	// Use this for initialization
	void Start () {
        quitDialog = quitDialog.GetComponent<Canvas>(); //assigned actual game object to variable
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        aboutButton = aboutButton.GetComponent<Button>();
        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
    }

    // ABOUT
    public void AboutPress()
    {
        aboutCanvas.enabled = true;

        //disabling
    }
	
	// EXIT
	public void ExitPress()
    {
        quitDialog.enabled = true;

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

        playButton.enabled = true;
        exitButton.enabled = true;
        controlsButton.enabled = true;
        optionsButton.enabled = true;
        aboutButton.enabled = true;
    }

    public void YesExitPress()
    {

    }

    public void StartLevel()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene("prototype");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
