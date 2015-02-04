using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{

	public GameObject snakehead;
	public GameObject snakeprefab;
	public GameObject gridCube;

	public int level = 1;

	public GameObject food;
	public GameObject poison;
	public Vector3[] foodPosition;
	public Vector3[] poisonPosition;

	public Camera mainCamera;

	// Use this for initialization
	void Start () 
	{
		// creates snake
		GameObject snake1 = (GameObject)Instantiate (snakehead, new Vector3(4, 4, 2), Quaternion.identity);
		snake1.name = "snake1";
		GameObject snake2 = (GameObject)Instantiate (snakeprefab, new Vector3(3, 4, 2), Quaternion.identity);
		snake2.name = "snake2";
		GameObject snake3 = (GameObject)Instantiate (snakeprefab, new Vector3(2, 4, 2), Quaternion.identity);
		snake3.name = "snake3";

		// creates the grid
		for (int i = 0; i < 10; i++) 
		{
			for (int j = 0; j < 10; j++) 
			{
				for (int k = 0; k < 10; k++) 
				{
					Instantiate(gridCube, new Vector3(i, j, k), Quaternion.identity);
					//GameObject.Find("gridCube(Clone)").gameObject.transform.parent = GameObject.Find("_LevelManager_").transform;
					GameObject.Find("gridCube(Clone)").gameObject.name = "gridCell(" + i + ", " + j + ", " + k + ")";
					
				}
			}
		}

		// creates initial food
		//level = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel;
		int foodNumber = 3 /*+ level / 2*/;
		foodPosition = new Vector3[foodNumber];
		for (int i = 0; i < foodNumber; i++) 
		{
			float px = (float) Random.Range(0,10);
			float py = (float) Random.Range(0,10);
			float pz = (float) Random.Range(0,10);
			foodPosition[i] = new Vector3(px, py, pz);
			Instantiate(food, foodPosition[i], Quaternion.identity);
		}
		// creates poison
		int poisonNumber = level;
		poisonPosition = new Vector3[poisonNumber];
		for (int i = 0; i < poisonNumber; i++) 
		{
			float px = (float) Random.Range(0,10);
			float py = (float) Random.Range(0,10);
			float pz = (float) Random.Range(0,10);
			poisonPosition[i] = new Vector3(px, py, pz);
			Instantiate(poison, poisonPosition[i], Quaternion.identity);
		}	
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		GameObject[] snakeBody;
		bool inside = false;
		int snakeLength = GameObject.Find ("snake1").GetComponent<Move2> ().snakeLength;
		for (int i = 1; i <= snakeLength; i++) 
		{
			inside = GeometryUtility.TestPlanesAABB (GeometryUtility.CalculateFrustumPlanes (mainCamera), GameObject.Find ("snake" + i).GetComponent<BoxCollider>().bounds);
			if (inside) 
			{
				break;
			}
		}
		if (!inside) 
		{
			GameObject.Find ("loserMessage").guiText.enabled = true;
			
			(GameObject.Find ("snake1").GetComponent<Move2>()).enabled = false;
			GameObject.Find ("_GameManager_").GetComponent<GameManager>().Dead();
			snakeBody = GameObject.FindGameObjectsWithTag ("Snake");			
			for (int i = 0; i < snakeBody.Length; i++) 
			{
				Destroy (snakeBody [i]);
			}
			snakeBody = GameObject.FindGameObjectsWithTag ("Tail");			
			for (int i = 0; i < snakeBody.Length; i++) 
			{
				Destroy (snakeBody [i]);
			}
		}
		
	}
}
