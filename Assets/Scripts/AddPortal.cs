using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPortal : MonoBehaviour {

	// Use this for initialization
    public float distance;
    public GameObject backgroundIn;
    public GameObject backgroundOut;
    public Sprite inPortal;
    public Sprite outPortalUp;
    public Sprite outPortalDown;
    public Sprite outPortalLeft;
    public Sprite outPortalRight;
    private bool input = true;
    private bool outputPortal = false;
    private InputPortal por;
    private OutputPortal outPor;
    private int id = 0;
    private Vector3 pos;
    private bool mouseDown = false;
    public List<Tuple> portals = new List<Tuple>();
    private Color portalColor;
    private GameObject currentOutputPortal;
    private bool pause = true;
    private bool inMenu = true;

    void Start () {
        distance = 1.0f;
        portalColor = new Color(Random.value, Random.value, Random.value, 1.0f); 
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.P) && !inMenu)
        {
            pause = !pause;
        }
    }

    void OnMouseDown()
    {
        if (!pause)
        {

            Debug.Log("pause" + pause);
            var v3 = Input.mousePosition;
            v3.z = distance;
            pos = Camera.main.ScreenToWorldPoint(v3);
            if (pos.x > 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 + 1;
            if (pos.x <= 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 - 1;
            if (pos.y < 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 - 1;
            if (pos.y >= 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 + 1;
            Debug.Log("OnMouseDown " + pos);
            mouseDown = true;
            if (input)
            {
                GameObject obj = (GameObject)Instantiate(backgroundIn, pos, Quaternion.identity);
                obj.AddComponent<RemovePortal>();
                obj.AddComponent<PortalId>();
                obj.GetComponent<Renderer>().material.color = portalColor;
                //obj.GetComponent<Renderer>().material.mainTexture = inPortal;
                obj.GetComponent<SpriteRenderer>().sprite = inPortal;

                PortalId idOfNewPortal = obj.GetComponent<PortalId>();
                idOfNewPortal.setId(id);
                por = new InputPortal(id);
                por.InputPortalGO = obj;
                input = false;
            }
            else
            {
                currentOutputPortal = (GameObject)Instantiate(backgroundOut, pos, Quaternion.identity);
                currentOutputPortal.AddComponent<RemovePortal>();
                currentOutputPortal.AddComponent<PortalId>();
                currentOutputPortal.GetComponent<Renderer>().material.color = portalColor;
                currentOutputPortal.GetComponent<SpriteRenderer>().sprite = inPortal;
                PortalId idOfNewPortal = currentOutputPortal.GetComponent<PortalId>();
                idOfNewPortal.setId(id);
                outputPortal = true;
                input = true;
            }

        }
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
                    if (posUp.x > pos.x) 
                    { 
                        vec = Vector2.right;
                        currentOutputPortal.GetComponent<SpriteRenderer>().sprite = outPortalRight;
                    }else{
                     vec = Vector2.left;
                     currentOutputPortal.GetComponent<SpriteRenderer>().sprite = outPortalLeft;
                    }
                }
                else
                {
                    if (posUp.y > pos.y)
                    {
                        vec = Vector2.up;
                        currentOutputPortal.GetComponent<SpriteRenderer>().sprite = outPortalUp;
                    }
                    else { vec = Vector2.down;
                    currentOutputPortal.GetComponent<SpriteRenderer>().sprite = outPortalDown;
                    }
                }
                Debug.Log("Direction: " + vec);
                mouseDown = false;          
                OutputPortal localOutputPortal = new OutputPortal(id, vec);
                localOutputPortal.setOutputPortal(currentOutputPortal);
                localOutputPortal.setPosition((int)pos.x, (int)pos.y);

                portals.Add(new Tuple(por, localOutputPortal));
                id++;
                portalColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            }
        }
    }

    public void setPause(bool pause)
    {
        this.pause = pause;
    }

    public void setInMenu(bool inMenu)
    {
        this.inMenu = inMenu;
    }

    public class InputPortal
    {
        int id;

        public GameObject InputPortalGO;

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
        private GameObject outputPortlaGameObject;

        public void setOutputPortal(GameObject outport) {
            outputPortlaGameObject = outport;
        }

        public GameObject getOutputPortal() {
            return outputPortlaGameObject;
        }

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
