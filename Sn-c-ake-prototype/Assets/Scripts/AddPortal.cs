using UnityEngine;
using System.Collections;

public class AddPortal : MonoBehaviour {

	// Use this for initialization
    public float distance;
    public GameObject newPortal;
    void Start () {
        distance = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        var v3 = Input.mousePosition;
        v3.z = distance;
        Vector2 pos = Camera.main.ScreenToWorldPoint(v3);
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        Debug.Log("Position portal" + pos);
        GameObject obj = (GameObject)Instantiate(newPortal, new Vector2(pos.x, pos.y), Quaternion.identity);
        
        obj.transform.Rotate(new Vector3(90f, 0f, 0f));
        obj.AddComponent<RemovePortal>();
    }
}
