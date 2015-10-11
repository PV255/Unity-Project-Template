using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public float moveTime;
    public float moveRate;
    public float moveDistance;
    public GameObject tailPrefab;

    Vector2 dir = Vector2.right;

    private List<Transform> tail = new List<Transform>();
    bool ate = false;
	// Use this for initialization
	void Start () {
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
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
            Debug.Log("collision with: " + coll.name);
        }
    }

    public void Move() {
        
        Vector2 currentPossition = transform.position;

        transform.Translate(dir * moveDistance);
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

    void SpawnFood()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}
