using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using SaveLoad;
public class StartMenu : MonoBehaviour {

    public Canvas quitDialog;
    public Canvas aboutCanvas;
public Canvas levelsCanvas;
    public Canvas controlsCanvas;
    public Canvas controls1Canvas;
    public Canvas optionsCanvas;
    public Canvas menuCanvas;
    public Canvas uiCanvas;
    public Canvas gameOver;
    public Button playButton;
    public Button exitButton;
    public Button controlsButton;
    public Button optionsButton;
    public Button aboutButton;
    public Button levelsButton;
    public AudioSource audioSource;
    public Toggle musicToggle;

    public GameObject snakeObject;
    public Snake snakeScript;
    public GameObject backgroundObject;
    public AddPortal addPortalScript;

	public Text lock1;
	public Text lock2;
	public Text lock3;
	public Text lock4;

	public Button level1;
	public Button level2;
	public Button level3;
	public Button level4;
    // Use this for initialization
    void Start () {
        
        quitDialog = quitDialog.GetComponent<Canvas>(); //assigned actual game object to variable
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
levelsCanvas = levelsCanvas.GetComponent<Canvas>();
        controlsCanvas = controlsCanvas.GetComponent<Canvas>();
        controls1Canvas = controls1Canvas.GetComponent<Canvas>();
        optionsCanvas = optionsCanvas.GetComponent<Canvas>();
        menuCanvas = menuCanvas.GetComponent<Canvas>();
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        gameOver = gameOver.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        controlsButton = controlsButton.GetComponent<Button>();
        optionsButton = optionsButton.GetComponent<Button>();
        aboutButton = aboutButton.GetComponent<Button>();
        levelsButton = levelsButton.GetComponent<Button>();
        audioSource = audioSource.GetComponent<AudioSource>();
        musicToggle = musicToggle.GetComponent<Toggle>();

        snakeObject = GameObject.FindGameObjectWithTag("Snake");
        snakeScript = snakeObject.GetComponent<Snake>();
        backgroundObject = GameObject.FindGameObjectWithTag("Background");
        addPortalScript = backgroundObject.GetComponent<AddPortal>();

lock1 = lock1.GetComponent<Text>();
		level1 = level1.GetComponent<Button>();
		lock2 = lock2.GetComponent<Text>();
		level2 = level2.GetComponent<Button>();
		lock3 = lock3.GetComponent<Text>();
		level3 = level3.GetComponent<Button>();
		lock4 = lock4.GetComponent<Text>();
		level4 = level4.GetComponent<Button>();

        audioSource.mute = true;
        quitDialog.enabled = false;
        aboutCanvas.enabled = false;
        controlsCanvas.enabled = false;
        controls1Canvas.enabled = false;
        optionsCanvas.enabled = false;
        uiCanvas.enabled = false;
        gameOver.enabled = false; 
        levelsCanvas.enabled = false;
    }

    public void ControlsPress()
    {
        controlsCanvas.enabled = true;
		quitDialog.enabled = false;
		aboutCanvas.enabled = false;
	
		optionsCanvas.enabled = false;
		uiCanvas.enabled = false;
		levelsCanvas.enabled = false;
controls1Canvas.enabled = true;
    }

public void LevelsPress()
	{
		levelsCanvas.enabled = true;

		setButton (level1, 0);
		setButton (level2, 1);
		setButton (level3, 2);
		setButton (level4, 3);

		setText (lock1, 0);
		setText (lock2, 1);
		setText (lock3, 2);
		setText (lock4, 3);
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
Loading.SaveGame ();
    
        MenuButtonsEnamble(false);
    }

    private void MenuButtonsEnamble(bool enable)
    {
        playButton.enabled = enable;
        exitButton.enabled = enable;
        controlsButton.enabled = enable;
        optionsButton.enabled = enable;
        aboutButton.enabled = enable;
        levelsButton.enabled = enabled;
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
		levelsCanvas.enabled = false;

        MenuButtonsEnamble(true);
    }

    public void NextControls()
    {
        controls1Canvas.enabled = false;
        controlsCanvas.enabled = true;
    }

public void ResetHighscore()
	{
		Loading.resetHighscore();
		setText (lock1, 0);
		setText (lock2, 1);
		setText (lock3, 2);
		setText (lock4, 3);
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

    public void GameOver()
    {
        gameOver.enabled = false;
        SceneManager.LoadScene(0);
        StartLevel();
    } public void SetLevel(int levelId)
	{
		Debug.Log ("SetLevel");
		levelsCanvas.enabled = false;
		snakeScript.SetLevel (levelId);
		StartLevel ();
	}

	private void setButton(Button levelButton, int id)
	{
		if (Loading.isLock (id)) {
			levelButton.interactable = false;
		} else {
			levelButton.interactable = true;
		}
	}

	private void setText(Text lockText, int id)
	{
		if(Loading.isLock(id))
		{
			lockText.text = "lock";
		}
		else
		{
			lockText.text = "highscore: " + Loading.getHighscore(id);
		}
	}

    //Exit - yes press
    public void ExitGame()
    {
		Loading.SaveGame();
        Application.Quit();
    }
}
