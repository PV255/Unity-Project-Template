using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class keyboardHintsScript : MonoBehaviour {

    public Image move;
    public Image jump;
    public Image attack;
    

	// Use this for initialization
	void Start () {
        jump.CrossFadeAlpha(0, 0, true);
        attack.CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            move.CrossFadeAlpha(0, 1, true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpaceBar pressed");
            jump.CrossFadeAlpha(0, 1, true);
            //jump.CrossFadeAlpha(0, 1, true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            attack.CrossFadeAlpha(0, 1, true);
        }
    }


}
