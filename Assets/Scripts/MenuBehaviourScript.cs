using UnityEngine;
using System.Collections;

public class MenuBehaviourScript : MonoBehaviour {
    public void newGame(){
        Application.LoadLevel(1);
    }
    
    public void quitGame(){
        Application.Quit();
    }
}
