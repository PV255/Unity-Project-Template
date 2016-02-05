using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInterface : MonoBehaviour {

    public Button exitButton;
    public Canvas menuCanvas;
    public Canvas uiCanvas;
    public Text playGame;

    public GameObject snakeObject;
    public Snake snakeScript;
    public GameObject backgroundObject;
    public AddPortal addPortalScript;

    public AudioSource audioSource;

    void Start () {
        exitButton = exitButton.GetComponent<Button>();
        menuCanvas = menuCanvas.GetComponent<Canvas>();
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        playGame = playGame.GetComponent<Text>();

        snakeObject = GameObject.FindGameObjectWithTag("Snake");
        snakeScript = snakeObject.GetComponent<Snake>();
        backgroundObject = GameObject.FindGameObjectWithTag("Background");
        addPortalScript = backgroundObject.GetComponent<AddPortal>();


        uiCanvas.enabled = false;
    }

    // Update is called once per frame

    public void ExitGame()
    {
        playGame.text = "continue";
        snakeScript.setPause(true);
        snakeScript.setInMenu(true);
        addPortalScript.setPause(true);
        addPortalScript.setInMenu(true);
        menuCanvas.enabled = true;
        uiCanvas.enabled = false;
        snakeScript.setInMenu(true);
        snakeScript.setPause(true);
        addPortalScript.setInMenu(true);
        addPortalScript.setPause(true);
    }
}
