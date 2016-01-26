using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {
    public void newGame(){
        if (GameManager.Instance != null) {
            for (int i = 0; i < 5; i++) {
                GameManager.Instance.AddLife();
            }
        }

        Application.LoadLevel(1);
    }

    public void gameOverLeave(){
        Application.LoadLevel(0);
    }

    public void quitGame(){
        Application.Quit();
    }
}
