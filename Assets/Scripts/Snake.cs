using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Snake : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject background;
    public AddPortal AddPortalSript;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public float distance;
    public float moveTime;
    public float moveRate;
    public float moveDistance;
    public GameObject tailPrefab;
    public GameObject newPortal;
    public GameObject iniPortal;
    public GameObject iniPortal2;
    Vector2 dir = Vector2.right;
    private List<Transform> tail = new List<Transform>();
    bool ate = false;
    bool portal = false;
	// Use this for initialization
	void Start () {
        background = GameObject.FindGameObjectWithTag("Background");
        AddPortalSript = background.GetComponent<AddPortal>();

        InvokeRepeating("Move", moveTime, moveRate);
        SpawnFood();
	}
	
	// Update is called once per frame
    void Update() {

        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector2.down;    
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left; 
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (portal) { return; }
        // Food?
        if (coll.name.StartsWith("foodPrefab"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else if (coll.name.StartsWith("InitialPor")) {
            portal = true;
            if (coll.name.StartsWith("InitialPortalPrefab2"))
            {
                transform.position = iniPortal.transform.position;
            }
            else {
                transform.position = iniPortal2.transform.position;
            }
        }else if(coll.name.StartsWith("PortalPrefab")){
             // = GetComponent(AddPortal); 
            List<AddPortal.Tuple> portals = AddPortalSript.portals;
            PortalId something = coll.gameObject.GetComponent<PortalId>();
            int id = something.id;
            Debug.Log("Collision portal id: "+id);
            AddPortal.Tuple onIndex = null;
            foreach (AddPortal.Tuple tup in portals){
                if (tup.outputPortal.getId() == id) {
                    onIndex = tup;
                    break;
                }
            }
            if (onIndex != null)
            {
                dir = onIndex.outputPortal.getHeading();
                transform.position = onIndex.outputPortal.getPosition();
            }

        }
        else if (coll.name.StartsWith("Tail"))
        {

        }

        else
        {
            // ToDo 'You lose' screen
            Debug.Log("collision with: " + coll.name);
            Debug.Log("Score: " +tail.Count);
            foreach(Transform ta in tail){
                Destroy(ta.gameObject);
            }
            tail = new List<Transform>();
            transform.position = new Vector2(-7, 1);
        }
    }

    public void Move() {
        portal = false;
        Vector2 currentPossition = transform.position;

        transform.Translate(dir * moveDistance);
        transform.Rotate(dir, 0, Space.World);
        if (ate) {
            SpawnFood();
            tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, Quaternion.identity)).transform);
            ate = false;
            
        }
        else if (tail.Count > 0) {
            tail.Last().position = currentPossition;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
                
           
        }
    }

    bool inBounds(Vector3 bounds) {
        if ((bounds.x > borderLeft.position.x) && (bounds.x < borderRight.position.x) && (bounds.y > borderBottom.position.y) && (bounds.y < borderTop.position.y)) { return true; }
        return false;
    }

    void OnMouseDown()
    {
        var v3 = Input.mousePosition;
        v3.z = distance;
        Vector3 pos = Camera.main.ScreenToWorldPoint(v3);
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        Debug.Log("Position" + pos);
        if (inBounds(pos))
        {
            GameObject obj = (GameObject)Instantiate(newPortal, pos, Quaternion.identity);
            //obj.transform.Rotate(new Vector3(90f, 0f, 0f));
            obj.AddComponent<RemovePortal>();
        }
    }

    void SpawnFood()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);
        Vector2 pos;
        pos.x = x;
        pos.y = y;
        Debug.Log("Position food" + pos);
        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}
