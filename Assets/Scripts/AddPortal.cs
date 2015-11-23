using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPortal : MonoBehaviour {

	// Use this for initialization
    public float distance;
    public GameObject inPortal;
    public GameObject outPortal;
    private bool input = true;
    private InputPortal por;
    private int id = 0;
    private Vector3 pos;
    private bool mouseDown = false;
    public List<Tuple> portals = new List<Tuple>();

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
        pos = Camera.main.ScreenToWorldPoint(v3);
        if (pos.x > 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 + 1;
        if (pos.x < 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 - 1;
        if (pos.y < 0) pos.y = Mathf.Round((int)pos.y/2) *2 - 1;
        if (pos.y > 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 + 1;
        Debug.Log("OnMouseDown " + pos);
        mouseDown = true;
        if (input)
        {
            GameObject obj = (GameObject)Instantiate(inPortal, pos, Quaternion.identity);
            obj.AddComponent<RemovePortal>();
            por = new InputPortal(id);
            input = false;
        }
        else {
            GameObject obj = (GameObject)Instantiate(outPortal, pos, Quaternion.identity);
            obj.AddComponent<RemovePortal>();
            Vector2 vec = Vector2.up;
            portals.Add(new Tuple(por, new OutputPortal(id, vec)));
            id++;
            input = true;
        }
            //obj.transform.Rotate(new Vector3(90f, 0f, 0f));
            //obj.AddComponent<RemovePortal>();
    }

    void OnMouseUp()
    {
        if (mouseDown)
        {
            var v3 = Input.mousePosition;
            v3.z = distance;
            Vector3 posUp = Camera.main.ScreenToWorldPoint(v3);
            //Debug.Log("OnMouseUp " + posUp);
            float distX = Mathf.Abs(posUp.x - pos.x);
            float distY = Mathf.Abs(posUp.y - pos.y);
            string direction;
            //Debug.Log("distX: " + distX + ", distY: " + distY);
            if (distX > distY)
            {
                if (posUp.x > pos.x) direction = "right";
                else direction = "left";
            }
            else
            {
                if (posUp.y > pos.y) direction = "up";
                else direction = "down";
            }
            Debug.Log("Direction: " + direction);
            mouseDown = false;
        }
    }

    
    public class InputPortal
    {
        int id;

        public InputPortal(int id) {
            this.id = id;
        }
        public int getId() {
            return this.id;
        }

    }

    public class OutputPortal
    {
        int id;
        Vector2 heading;

        public OutputPortal(int id, Vector2 to) {
            this.id = id;
            heading = to;
        }
    }

    public class Tuple
    {
        InputPortal inputPortal;
        OutputPortal outputPortal;

        public Tuple(InputPortal inp, OutputPortal outp){
            inputPortal = inp;
            outputPortal = outp;
        }
    }
}
