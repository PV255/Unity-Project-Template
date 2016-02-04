using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour {

    public Button exitButton;
    public Canvas quitDialog;

    public GameObject snakeObject;
    public Snake snakeScript;
    public GameObject backgroundObject;
    public AddPortal addPortalScript;

    void Start () {
        quitDialog = quitDialog.GetComponent<Canvas>();
        exitButton = exitButton.GetComponent<Button>();

        snakeObject = GameObject.FindGameObjectWithTag("Snake");
        snakeScript = snakeObject.GetComponent<Snake>();
        backgroundObject = GameObject.FindGameObjectWithTag("Background");
        addPortalScript = backgroundObject.GetComponent<AddPortal>();

        quitDialog.enabled = false;
    }

    // Update is called once per frame
    public void ExitPress()
    {
        quitDialog.enabled = true;
        exitButton.enabled = true;
        snakeScript.setPause(true);
        snakeScript.setInMenu(true);
        addPortalScript.setPause(true);
        addPortalScript.setInMenu(true);
    }

    public void Back()
    {
        quitDialog.enabled = false;
        exitButton.enabled = true;
        snakeScript.setPause(false);
        snakeScript.setInMenu(false);
        addPortalScript.setPause(false);
        addPortalScript.setInMenu(false);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("startMenu");
    }
}
