using UnityEngine;
using System.Collections;

public class creditsScroll : MonoBehaviour {
    public float scrollSpeed = 50f;

    private float targetY;

    public void resetY() {
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }

    void Start () {
	
	}
    
    void Update() {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + Time.deltaTime * scrollSpeed, 0, 10000);
        transform.position = pos;
	}
}
