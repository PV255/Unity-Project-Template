using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

using SimpleJSON;
using SaveLoad;

public class Snake : MonoBehaviour
{
    
    public GameObject foodPrefab;
    public GameObject decreaseSnakeLengthFoodPrefab;
    public GameObject incraseSnakeSpeedFoodPrefab;
    public GameObject decreaseSnakeSpeedPrefab;
    public GameObject background;
    public GameObject obstaclePrefab;
    public GameObject eraserObstacleGo;
    public GameObject pencilObstacleGO;
    public GameObject phoneObstacleGo;
    public Canvas gamePausedText;
    public Canvas gameOverScreen;
    public Text scoreText;
    public Text gameOverScoreText;
    public AddPortal AddPortalSript;
    public GameObject finalPOrtalGameObject;
    public AudioSource eatSound;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public float distance;
    public float moveTime;
    public float moveChangeRate;
    public float moveDistance;
    public GameObject tailPrefab;
    public GameObject lastTailPrefab;
    public GameObject initialBody;
    public GameObject initialTail;
    public int desiredLengthOfSnake;
    Vector2 dir = Vector2.left;
    private List<Transform> tail = new List<Transform>();
    bool ate = false;
    bool shrink = false;
    bool portal = false;
    bool pause = true;
    bool inMenu = true;
    private int fixedUpdateCounter = 0;
    private int specialFoodCount = 0;
    private bool isSpecialOnTable = false;
    private GameObject specialFoodObject;
    private float specialTime;
    private static int numOfRows = 12;
    private static int numOfColls = 20;

    private int score = 0;
    private static int foodScoreNormal = 10;
    private static int foodScoreIncSpeed = 20;
    private static int foodScoreDecrease = 5; //decrease speed or length
    private bool finalPortalOpen = false;
    private int nextLevel = 1;
    private GameObject finalPortalGO;
    private List<GameObject> obstaclesGO = new List<GameObject>();


    bool[][] obstacle = new bool[numOfRows][]; /*12*20*/
                                               // Use this for initialization
    class PortalToDelete {
        private static GameObject inputPortal;
        private static GameObject outputPortal;
        public int numberOfSteps;

        public void setInputPortal(GameObject input) {
            inputPortal = input;
        }
        public GameObject getInputPortal() {
            return inputPortal;
        }
        public void setOutPortal(GameObject output){
            outputPortal = output;
        }
        public GameObject getOutputPortal() {
            return outputPortal;
        }
    
    }

    void Start()
    {
		Loading.LoadLevels();
        background = GameObject.FindGameObjectWithTag("Background");
        gamePausedText = gamePausedText.GetComponent<Canvas>();
        gamePausedText.enabled = false;
        scoreText = scoreText.GetComponent<Text>();
        gameOverScoreText = gameOverScoreText.GetComponent<Text>();
        gameOverScreen = gameOverScreen.GetComponent<Canvas>();
        AddPortalSript = background.GetComponent<AddPortal>();
        eatSound = eatSound.GetComponent<AudioSource>();
        eatSound.volume = 0;
        fixedUpdateCounter = 0;
        for (int i = 0; i < numOfRows; i++)
        {
            obstacle[i] = new bool[numOfColls];
        }
        forgetObstacles();
		setObstacles(Loading.GetBariers(Loading.getLastLevelId()));
        setPictures(Loading.GetPictures(Loading.getLastLevelId()));
        tail.Add(initialBody.transform);
        tail.Add(initialTail.transform);
        SpawnFood();

    }
  
    /*guarantees same time between each update*/
    void FixedUpdate()
    {
        if (!pause)
        {
            if (tail.Count == desiredLengthOfSnake) {
                if (!finalPortalOpen) {
                    //otvorit vstupny portal jeden 
                    finalPortalOpen = true;
                    OpenFinalPortal();
                }
            }
            fixedUpdateCounter++;
            float time = (float)moveTime / 0.02f;
            if ((int)time == fixedUpdateCounter)
            {
                fixedUpdateCounter = 0;
                Move();
            }
            if (isSpecialOnTable)
            {
                specialFoodCount++;
                //Debug.Log("Actual time: " + specialFoodCount + " looking for: " + (int)specialTime);
                if (specialFoodCount == (int)specialTime)
                {
                    removeSpecialFood();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Move in a new Direction?
        if (Input.GetKeyUp(KeyCode.P) && !inMenu)
        {
            pause = !pause;
            gamePausedText.enabled = !gamePausedText.enabled;
        }
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
            score += foodScoreNormal;
            Loading.setScore(Loading.getLastLevelId(), score);
            scoreText.text = score.ToString();
            eatSound.Play();
        }
        else if (coll.name.StartsWith(decreaseSnakeLengthFoodPrefab.name))
        {
            // Get shorter in next Move call
            shrink = true;

            // Remove the Food
            Destroy(coll.gameObject);
            score += foodScoreDecrease;
            Loading.setScore(Loading.getLastLevelId(), score);
            scoreText.text = score.ToString();
            eatSound.Play();
        }
        else if (coll.name.StartsWith(incraseSnakeSpeedFoodPrefab.name))
        {
            //increase speed of snake ??do we increase the lenght of snake as well??
            UpdateSpeed(true);
            removeSpecialFood();
            //score updated in UpdateSpeed();
        }
        else if (coll.name.StartsWith(decreaseSnakeSpeedPrefab.name))
        {
            //decrease speed
            UpdateSpeed(false);

            removeSpecialFood();

            //score updated in UpdateSpeed();
        }
        else if (coll.name.StartsWith(AddPortalSript.backgroundIn.name))
        {
            portal = true;
            //Debug.Log("hura portal!");
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
                //Debug.Log(transform.rotation);
                transform.position = onIndex.outputPortal.getPosition();

                PortalToDelete local = new PortalToDelete();
                local.setInputPortal(coll.gameObject);
                local.setOutPortal(onIndex.outputPortal.getOutputPortal());
                local.numberOfSteps = 0;

            }

        }
        else if (coll.name.StartsWith(AddPortalSript.backgroundOut.name)) {
        } else if (coll.name.StartsWith(finalPOrtalGameObject.name)) {
            desiredLengthOfSnake *= 2;
            SetLevel(nextLevel);
            nextLevel = (nextLevel + 1) / 4;
            
        }


        else
        {
            Loading.setScore(Loading.getLastLevelId(), score);
            gameOver();
            score = 0;
            scoreText.text = score.ToString();
            // ToDo 'You lose' screen
            Debug.Log("collision with: " + coll.name);
            Debug.Log("Score: " + tail.Count);
            //initialize();
        }
    }



    public void Move()
    {
        //Debug.Log("hybem sa!");
        GameObject lastPosition;
        portal = false;
        Vector2 currentPossition = transform.position;
        Vector2 position;
        Quaternion rotation;
        transform.Translate(dir * moveDistance, Space.World);
    

        if (ate)
        {
            SpawnFood();
            if (tail.Count == 0) {
                tail.Add(((GameObject)Instantiate(lastTailPrefab, currentPossition, transform.rotation)).transform);
            }
            else
            {
                tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, transform.rotation)).transform);
            }
            ate = false;

        }
        else if (shrink)
        {
            if (tail.Count >= 2)
            {
                isSpecialOnTable = false;
                shrink = false;
               //* tail.Insert(0, tail.Last());
                if (tail.Count >= 3)
                {
                    lastPosition = tail.Last().gameObject;
                    Destroy(lastPosition);
                    tail.RemoveAt(tail.Count - 1);
                    lastPosition = tail.Last().gameObject;
                    Destroy(lastPosition);
                    tail.RemoveAt(tail.Count - 1);
                    position = tail.Last().position;
                    rotation = tail.Last().rotation;
                    lastPosition = tail.Last().gameObject;
                    Destroy(lastPosition);
                    tail.RemoveAt(tail.Count - 1);
                    tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, transform.rotation)).transform);
                    tail.Add(((GameObject)Instantiate(lastTailPrefab, position, rotation)).transform);
                }
                else {
                    //gameOver();
                    tail.Last().position = currentPossition;
                    tail.Last().rotation = transform.rotation;
                    tail.Insert(0, tail.Last());
                    tail.RemoveAt(tail.Count - 1);
                    lastPosition = tail.Last().gameObject;
                    Destroy(lastPosition);
                    tail.RemoveAt(tail.Count - 1);
                    
                }
                //tail.Insert(tail.Count - 1, ();
                //tail.Last().GetComponent<SpriteRenderer>().sprite = lastTailPrefab.GetComponent<SpriteRenderer>().sprite;
                SpawnFood();
            }
            else {
                /*GAME OVER dlzka hada je < 0*/
                gameOver();
            }
        }
        else if (tail.Count >=2 )
        {
            
                lastPosition = tail.Last().gameObject;
                Destroy(lastPosition);
                tail.RemoveAt(tail.Count - 1);
                position = tail.Last().position;
                rotation = tail.Last().rotation;
                lastPosition = tail.Last().gameObject;
                Destroy(lastPosition);
                tail.RemoveAt(tail.Count - 1);
                tail.Insert(0, ((GameObject)Instantiate(tailPrefab, currentPossition, transform.rotation)).transform);
                tail.Add(((GameObject)Instantiate(lastTailPrefab, position, rotation)).transform);
            
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
            if (moveTime <= 0.1) {
                moveTime = 0.1f;
                score += foodScoreNormal;
            }
            else score += foodScoreIncSpeed;
        }
        else {
            moveTime += moveChangeRate;
            score += foodScoreDecrease;
        }
        scoreText.text = score.ToString();
        eatSound.Play();
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
            //Debug.Log("suradnice do pola prekazok x: " + foodX + " y: " + (numOfRows - foodY - 1));
        } while (!(isSafeFoodPlant(foodX, numOfRows - foodY - 1)));
        // Debug.Log("Position food: " + pos);
        // Instantiate the food at (x, y)
        //need to shift the number
        if ((int)Random.Range(0,20) > 16)
        {
            isSpecialOnTable = true;
            specialFoodCount = 0;
            /*0,1,2*/
            int ran = (int)Random.Range(0, 3);
            Debug.Log("random: " + ran);
            if (ran == 0) {
                specialFoodObject = (GameObject)Instantiate(decreaseSnakeLengthFoodPrefab,
                        new Vector2(pos.x, pos.y),
                        Quaternion.identity);
            }
            else if (ran == 2) {
                specialFoodObject = (GameObject)Instantiate(decreaseSnakeSpeedPrefab,
                        new Vector2(pos.x, pos.y),
                        Quaternion.identity);
            }
            else {
                specialFoodObject = (GameObject)Instantiate(incraseSnakeSpeedFoodPrefab,
                        new Vector2(pos.x, pos.y),
                        Quaternion.identity);
            }
            float rand = Random.Range(4, 7);
            specialTime = rand / 0.02f;
        }
        else
        {
            Instantiate(foodPrefab,
                        new Vector2(pos.x, pos.y),
                        Quaternion.identity); // default rotation
        }
    }

    int whichInRange(int start, int position, int step)
    {
        //Debug.Log("start: " + start + " position: " + position + " step: " + step);
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

    void OpenFinalPortal() {
        Vector2 position = new Vector2(1,1);
        finalPortalGO = (GameObject) Instantiate(finalPOrtalGameObject, position, Quaternion.identity);
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
        foreach (GameObject obj in obstaclesGO) {
            Debug.Log("Destorying: " + obj);
            Destroy(obj);
            
        }
        obstaclesGO = new List<GameObject>();
    }

void setObstacles(JSONNode bariers)
	{
        int x, y;
        int xDist = Mathf.Abs((int)(borderRight.position.x - borderLeft.position.x));
        int yDist = Mathf.Abs((int)(borderBottom.position.y - borderTop.position.y));

        Debug.Log ("setObstacles");
		for (int i = 0; i < bariers.Count; i++)
		{
            x = bariers[i]["x"].AsInt;
            y = bariers[i]["y"].AsInt;
            obstacle[y][x] = true;
            Vector2 position = new Vector2();
            position.x =Mathf.Round( borderLeft.position.x + (x * xDist / numOfColls) +1);
            position.y =Mathf.Round( borderTop.position.y - (y * yDist / numOfRows) -1 );
            //obstaclesGO.Add((GameObject)Instantiate(obstaclePrefab, position, Quaternion.identity));
			Debug.Log ("obstacle"+ obstacle[y][x] + "position: "+position);
		}
	}

    void setPictures(JSONNode pictures)
    {
        Debug.Log("setPictures");
        int xDist = Mathf.Abs((int)(borderRight.position.x - borderLeft.position.x));
        int yDist = Mathf.Abs((int)(borderBottom.position.y - borderTop.position.y));

        for (int i = 0; i < pictures.Count; i++)
        {
            //todo add sprite
            int type = pictures[i]["type"].AsInt;
            int x = pictures[i]["x"].AsInt;
            int y = pictures[i]["y"].AsInt;
            Vector2 position = new Vector2();
            position.x = Mathf.Round(borderLeft.position.x + (x * xDist / numOfColls) + 1);
            position.y = Mathf.Round(borderTop.position.y - (y * yDist / numOfRows) - 1);

            switch (type)
            {
                case 0:
                    //todo hrncek
                    for (int k = 0; k < 5; k++) {
                        for (int j = 0; j < 4; j++) {
                            position.x = Mathf.Round(borderLeft.position.x + (x * xDist / numOfColls) + 1 + j *2);
                            position.y = Mathf.Round(borderTop.position.y - (y * yDist / numOfRows) - 1 - k * 2);
                            obstaclesGO.Add((GameObject)Instantiate(obstaclePrefab, position, Quaternion.identity));

                        }
                    }
                    
                    break;
                case 1:
                    //todo guma
                    position.y -= 1;
                    obstaclesGO.Add((GameObject)Instantiate(eraserObstacleGo, position, Quaternion.identity));
                    break;
                case 2:
                    //todo ceruzka
                    position.x -= 0.78f;
                    position.y -= 0.24f;
                    obstaclesGO.Add((GameObject)Instantiate(pencilObstacleGO, position, Quaternion.identity));
                    break;
                case 3:
                    //todo mobil
                    position.x -= 0.78f;
                    position.y -= 0.24f;
                    obstaclesGO.Add((GameObject)Instantiate(phoneObstacleGo, position, Quaternion.identity));
                    break;
            }
        }
    }

    void removeSpecialFood() {
        Destroy(specialFoodObject);
        isSpecialOnTable = false;
        SpawnFood();
    }

    public void setPause(bool pause)
    {
        this.pause = pause;
    }

    public void setInMenu(bool inMenu)
    {
        this.inMenu = inMenu;
    }

    public bool isPaused()
    {
        return gamePausedText.enabled;
    }

public void SetLevel(int levelId)
	{
        Destroy(finalPortalGO);
        forgetObstacles();
		Loading.setLastLevelId (levelId);
		setObstacles(Loading.GetBariers(levelId));
        setPictures(Loading.GetPictures(Loading.getLastLevelId()));
    }
    /*TODO: implement game over screen here*/
    void gameOver()
    {
        forgetObstacles();

        pause = true;
        inMenu = true;
        AddPortalSript.setPause(true);
        AddPortalSript.setInMenu(true);
        gameOverScoreText.text = score.ToString();
        gameOverScreen.enabled = true;
    }

    public void loadSceneZero()
    {
        SceneManager.LoadScene(0);
    }

}
