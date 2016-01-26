using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {
    public Transform camera = null;

    public void newGame(){
        Application.LoadLevel(1);
        GameManager.Instance.newGameStarted();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    Vector3 pos = new Vector3(0, 0, 0);
    Vector3 y = new Vector3(0, 1, 0);

    void Update() {
        if (camera != null) {
            camera.RotateAround(pos, y, Time.deltaTime);
        }
    }

    public void gameOverLeave(){
        Application.LoadLevel(0);
    }
}
