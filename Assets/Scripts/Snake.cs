using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Snake : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject decreaseSnakeLengthFoodPrefab;
    public GameObject incraseSnakeSpeedFoodPrefab;
    public GameObject decreaseSnakeSpeedPrefab;
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
    Vector2 dir = Vector2.left;
    private List<Transform> tail = new List<Transform>();
    bool ate = false;
    bool shrink = false;
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
        if (coll.name.StartsWith(foodPrefab.name))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        if (coll.name.StartsWith(decreaseSnakeLengthFoodPrefab.name))
        {
            // Get longer in next Move call
            shrink = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else if(coll.name.StartsWith("PortalPrefab")){
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

                Vector3 head = new Vector3(dir.x,dir.y,0);
                Vector3 moveDirection = head;
                //transform.rotation = Quaternion.identity;
                if (moveDirection != Vector3.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    if (angle < 0)
                    {
                        angle = -1 * Mathf.Abs(Mathf.RoundToInt(angle)) / 90 * 90;
                    }
                    else
                    {
                        angle =Mathf.RoundToInt(angle)  / 90 * 90;
                    }
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                }
                dir = onIndex.outputPortal.getHeading();
                //transform.position = new Vector2(0,0);
                if (dir == Vector2.left) {
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                }
                if (dir == Vector2.right) {
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                }
                if (dir == Vector2.down)
                {
                    transform.rotation = new Quaternion(1, 1, 0, 0);
                }
                if (dir == Vector2.up)
                {
                    transform.rotation = new Quaternion(1, -1, 0, 0);
                }
                Debug.Log(transform.rotation);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation,new Quaternion(head.x,head.y,head.z,1) ,360);
                //transform.rotation = Quaternion.LookRotation(new Vector3(0,dir.x,dir.y));
                //transform.LookAt(onIndex.outputPortal.getHeading());
                transform.position = onIndex.outputPortal.getPosition();
                
            }

        }
        else if (coll.name.StartsWith(tailPrefab.name))
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

        transform.Translate(dir * moveDistance, Space.World);

        if (ate)
        {
            SpawnFood();
            tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, transform.rotation)).transform);
            ate = false;

        }
        else if (shrink) {
            if (tail.Count > 0)
            {
                tail.Last().position = currentPossition;
                tail.Last().rotation = transform.rotation;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
                tail.RemoveAt(tail.Count - 1);
            }
            else {
                /*GAME OVER dlzka hada je 0*/
            }
        } else if (tail.Count > 0)
        {
            tail.Last().position = currentPossition;
            tail.Last().rotation = transform.rotation;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);


        }
    }

    bool inBounds(Vector3 bounds) {
        if ((bounds.x > borderLeft.position.x) && (bounds.x < borderRight.position.x) && (bounds.y > borderBottom.position.y) && (bounds.y < borderTop.position.y)) { return true; }
        return false;
    }



    void SpawnFood()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x-2);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y-2,
                                  borderTop.position.y);
        Vector2 pos;
        pos.x = x;
        pos.y = y;
        if (pos.x > 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 + 1;
        if (pos.x < 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 - 1;
        if (pos.y < 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 - 1;
        if (pos.y > 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 + 1;
        Debug.Log("Position food" + pos);
        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(pos.x, pos.y),
                    Quaternion.identity); // default rotation
    }
}
