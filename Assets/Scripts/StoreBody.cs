using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreBody : MonoBehaviour {
	public List<Object> foodObject; 
	// Use this for initialization
	void Start () {
		foodObject = new List<Object>();
		foodObject.Add (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
