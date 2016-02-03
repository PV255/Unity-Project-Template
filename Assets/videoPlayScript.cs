using UnityEngine;
using System.Collections;

public class videoPlayScript : MonoBehaviour {

    public MovieTexture video;

	// Use this for initialization
	void Start () {
        video.Play();
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), video);
    }
}
