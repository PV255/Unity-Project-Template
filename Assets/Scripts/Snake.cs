using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Snake : MonoBehaviour
{

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
    public float moveChangeRate;
    public float moveDistance;
    public GameObject tailPrefab;
    public GameObject newPortal;

    Vector2 dir = Vector2.left;
    private List<Transform> tail = new List<Transform>();
    bool ate = false;
    bool shrink = false;
    bool portal = false;
    private int fixedUpdateCounter = 0;

    private static int numOfRows = 12;
    private static int numOfColls = 20;


    bool[][] obstacle = new bool[numOfRows][]; /*12*20*/
                                               // Use this for initialization
    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        AddPortalSript = background.GetComponent<AddPortal>();
        fixedUpdateCounter = 0;
        for (int i = 0; i < numOfRows; i++)
        {
            obstacle[i] = new bool[numOfColls];
        }
        forgetObstacles();
        SpawnFood();

    }

    /*guarantees same time between each update*/
    void FixedUpdate()
    {
        fixedUpdateCounter++;
        float time = (float)moveTime / 0.02f;
        if ((int)time == fixedUpdateCounter)
        {
            fixedUpdateCounter = 0;
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        if (coll.name.StartsWith(foodPrefab.name))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        else if (coll.name.StartsWith(decreaseSnakeLengthFoodPrefab.name))
        {
            // Get shorter in next Move call
            shrink = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        else if (coll.name.StartsWith(incraseSnakeSpeedFoodPrefab.name))
        {
            //increase speed of snake ??do we increase the lenght of snake as well??
            UpdateSpeed(true);
            Destroy(coll.gameObject);
            SpawnFood();

        }
        else if (coll.name.StartsWith(decreaseSnakeSpeedPrefab.name))
        {
            //decrease speed
            UpdateSpeed(false);
            SpawnFood();
            Destroy(coll.gameObject);
        }
        else if (coll.name.StartsWith(newPortal.name))
        {
            portal = true;
            Debug.Log("hura portal!");
            List<AddPortal.Tuple> portals = AddPortalSript.portals;
            PortalId something = coll.gameObject.GetComponent<PortalId>();
            int id = something.id;
            Debug.Log("Collision portal id: " + id);
            AddPortal.Tuple onIndex = null;
            foreach (AddPortal.Tuple tup in portals)
            {
                if (tup.outputPortal.getId() == id)
                {
                    onIndex = tup;
                    break;
                }
            }
            if (onIndex != null)
            {

                Vector3 head = new Vector3(dir.x, dir.y, 0);
                Vector3 moveDirection = head;
                if (moveDirection != Vector3.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    if (angle < 0)
                    {
                        angle = -1 * Mathf.Abs(Mathf.RoundToInt(angle)) / 90 * 90;
                    }
                    else
                    {
                        angle = Mathf.RoundToInt(angle) / 90 * 90;
                    }
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                }
                dir = onIndex.outputPortal.getHeading();
                if (dir == Vector2.left)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                }
                if (dir == Vector2.right)
                {
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
                transform.position = onIndex.outputPortal.getPosition();

            }

        }
        else if (coll.name.StartsWith(tailPrefab.name))
        {

        }

        else
        {
            gameOver();
            // ToDo 'You lose' screen
            Debug.Log("collision with: " + coll.name);
            Debug.Log("Score: " + tail.Count);
            foreach (Transform ta in tail)
            {
                Destroy(ta.gameObject);
            }
            tail = new List<Transform>();

            transform.position = new Vector2(-7, 1);
        }
    }

    public void Move()
    {
        portal = false;
        Vector2 currentPossition = transform.position;

        transform.Translate(dir * moveDistance, Space.World);

        if (ate)
        {
            SpawnFood();
            tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, transform.rotation)).transform);
            ate = false;

        }
        else if (shrink)
        {
            if (tail.Count > 0)
            {
                tail.Last().position = currentPossition;
                tail.Last().rotation = transform.rotation;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
                tail.RemoveAt(tail.Count - 1);
                SpawnFood();
            }
            else {
                /*GAME OVER dlzka hada je 0*/
                gameOver();
            }
        }
        else if (tail.Count > 0)
        {
            tail.Last().position = currentPossition;
            tail.Last().rotation = transform.rotation;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);


        }
    }

    bool inBounds(Vector3 bounds)
    {
        if ((bounds.x > borderLeft.position.x) && (bounds.x < borderRight.position.x) && (bounds.y > borderBottom.position.y) && (bounds.y < borderTop.position.y)) { return true; }
        return false;
    }

    void UpdateSpeed(bool increase)
    {
        if (increase)
        {
            moveTime -= moveChangeRate;
            if (moveTime <= 0.1) { moveTime = 0.1f; }

        }
        else {
            moveTime += moveChangeRate;
        }
        fixedUpdateCounter = 0;
    }

    void SpawnFood()
    {
        Vector2 pos;
        int foodX, foodY;
        do
        {
            // x position between left & right border
            int x = (int)Random.Range(borderLeft.position.x,
                                      borderRight.position.x + 1);

            // y position between top & bottom border
            int y = (int)Random.Range(borderBottom.position.y + 1,
                                      borderTop.position.y);

            pos.x = x;
            pos.y = y;
            x = Mathf.Abs((int)(borderRight.position.x - borderLeft.position.x));
            y = Mathf.Abs((int)(borderBottom.position.y - borderTop.position.y));
            //Debug.Log("rozdiely na borderoch, x: " + x + " y: " + y);
            if (pos.x > 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 - 1;
            if (pos.x <= 0) pos.x = Mathf.Round((int)pos.x / 2) * 2 + 1;
            if (pos.y <= 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 + 1;
            if (pos.y > 0) pos.y = Mathf.Round((int)pos.y / 2) * 2 - 1;
            //while popoziciach je na mieste x a y sa teraz nachadza rozpetie medzi hranicami
            //foodX = (int)Mathf.Round(pos.x) / 2 + numOfRows / 2 ;
            foodY = whichInRange((int)Mathf.Round(borderBottom.position.y), (int)Mathf.Round(pos.y), y / numOfRows);
            foodX = whichInRange((int)Mathf.Round(borderLeft.position.x), (int)Mathf.Round(pos.x), x / numOfColls);
            Debug.Log("suradnice do pola prekazok x: " + foodX + " y: " + (numOfRows - foodY - 1));
        } while (!(isSafeFoodPlant(foodX, numOfRows - foodY - 1)));
        Debug.Log("Position food: " + pos);
        // Instantiate the food at (x, y)
        //need to shift the number
        Instantiate(incraseSnakeSpeedFoodPrefab,
                    new Vector2(pos.x, pos.y),
                    Quaternion.identity); // default rotation
    }

    int whichInRange(int start, int position, int step)
    {
        Debug.Log("start: " + start + " position: " + position + " step: " + step);
        int current = start + step;
        int i = 0;
        while (current <= position)
        {
            current += step;
            i++;
        }
        return i;
    }

    /*in case there are obstacles on board, we need to check whether the food is not going to be planted in some corner, since the snake can't rotate it's head*/
    bool isSafeFoodPlant(int x, int y)
    {
        if (((x == 0) && (y == 0)) || ((x == numOfRows) && (y == 0)) ||
            ((x == numOfRows) && (y == numOfColls)) || ((x == 0) && (y == numOfColls)))
        {
            return false; //corners of game plan
        }
        else if (obstacle[y][x] == true)
        {
            return false; //hit with the obstacle
        }
        else if ((y == 0) || (y == numOfColls))
        {
            if ((obstacle[y][x - 1]) || obstacle[y][x + 1]) { return false; } // food by vertical border
        }
        else if ((x == 0) || (x == numOfRows))
        {
            if ((obstacle[y - 1][x]) || (obstacle[y + 1][x])) { return false; } // food by horizontal border
        }
        else if ((obstacle[y - 1][x] && obstacle[y][x - 1]) || (obstacle[y - 1][x] && obstacle[y][x + 1]) ||
            (obstacle[y][x - 1] && obstacle[y + 1][x]) || (obstacle[y][x - 1] && obstacle[y + 1][x + 1]))
        {
            return false;
        }
        return true;
    }

    void forgetObstacles()
    {
        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = 0; j < numOfColls; j++)
            {
                obstacle[i][j] = false;
            }
        }
    }

    /*TODO: implement game over screen here*/
    void gameOver()
    {
    }
}
