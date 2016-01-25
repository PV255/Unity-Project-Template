using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPortal : MonoBehaviour {

	// Use this for initialization
    public float distance;
    public GameObject inPortal;
    public GameObject outPortal;
    private bool input = true;
    private bool outputPortal = false;
    private InputPortal por;
    private OutputPortal outPor;
    private int id = 0;
    private Vector3 pos;
    private bool mouseDown = false;
    public List<Tuple> portals = new List<Tuple>();
    private Color portalColor;

    void Start () {
        distance = 1.0f;
        portalColor = new Color(Random.value, Random.value, Random.value, 1.0f); 
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
        if (pos.x <= 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 - 1;
        if (pos.y < 0) pos.y = Mathf.Round((int)pos.y/2) *2 - 1;
        if (pos.y >= 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 + 1;
        Debug.Log("OnMouseDown " + pos);
        mouseDown = true;
        if (input)
        {
            GameObject obj = (GameObject)Instantiate(inPortal, pos, Quaternion.identity);
            obj.AddComponent<RemovePortal>();
            obj.AddComponent<PortalId>();
            obj.GetComponent<Renderer>().material.color = portalColor;

            PortalId idOfNewPortal = obj.GetComponent<PortalId>();
            idOfNewPortal.setId(id);
            por = new InputPortal(id);
            input = false;
        }
        else {
            GameObject obj = (GameObject)Instantiate(outPortal, pos, Quaternion.identity);
            obj.AddComponent<RemovePortal>();
            obj.GetComponent<Renderer>().material.color = portalColor;
            outputPortal = true;
            input = true;
        }
            //obj.transform.Rotate(new Vector3(90f, 0f, 0f));
            //obj.AddComponent<RemovePortal>();
    }

    void OnMouseUp()
    {
        if (outputPortal)
        {
            if (mouseDown)
            {
                outputPortal = false;
                var v3 = Input.mousePosition;
                v3.z = distance;
                Vector3 posUp = Camera.main.ScreenToWorldPoint(v3);
                //Debug.Log("OnMouseUp " + posUp);
                float distX = Mathf.Abs(posUp.x - pos.x);
                float distY = Mathf.Abs(posUp.y - pos.y);
                //Debug.Log("distX: " + distX + ", distY: " + distY);
                Vector2 vec;
                if (distX > distY)
                {
                    if (posUp.x > pos.x) vec = Vector2.right;
                    else vec = Vector2.left;
                }
                else
                {
                    if (posUp.y > pos.y) vec = Vector2.up;
                    else vec = Vector2.down;
                }
                Debug.Log("Direction: " + vec);
                mouseDown = false;          
                OutputPortal localOutputPortal = new OutputPortal(id, vec);
                localOutputPortal.setPosition((int)pos.x, (int)pos.y);
                portals.Add(new Tuple(por, localOutputPortal));
                id++;
                portalColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            }
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
        int x, y;

        public Vector3 getPosition() { 
            return new Vector3(x,y,0) ;
        }

        public void setPosition(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public OutputPortal(int id, Vector2 to) {
            this.id = id;
            heading = to;
        }

        public Vector2 getHeading(){
            return heading;
        }
        public int getId() {
            return id;
        }
    }

    public class Tuple
    {
        public InputPortal inputPortal;
        public OutputPortal outputPortal;

        public Tuple(InputPortal inp, OutputPortal outp){
            inputPortal = inp;
            outputPortal = outp;
        }
    }
}
