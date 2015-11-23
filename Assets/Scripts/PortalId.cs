using UnityEngine;
using System.Collections;

public class PortalId : MonoBehaviour {
    public int id;
	// Use this for initialization
	void Start () {
	
	}
    public void setId(int id) {
        this.id = id;
    }

    int getId() {
        return this.id;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
