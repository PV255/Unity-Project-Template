using UnityEngine;
using System.Collections;

// destroys an instance of food when the snake collides with it,
// creates another instance of food somewhere else
// and appends another segment to snake's body
public class DestroyMe : MonoBehaviour 
{
	/*public GameObject food;
	public GameObject snakeprefab;
	private bool alive = true;
	public AudioClip eating;
	//public int score;

	void Start()
	{
	}
	// Use this for initialization
	void OnTriggerEnter(Collider c)
	{
		if (alive) 
		{
			alive = false;
			audio.PlayOneShot(eating);

			float px = (float) Random.Range(0,10);
			float py = (float) Random.Range(0,10);
			float pz = (float) Random.Range(0,10);
			Vector3 foodPosition = new Vector3 (px, py, pz);
			Instantiate (food, foodPosition, Quaternion.identity);

			Vector3 NewBodyPosition = GameObject.Find("snake" + GameObject.Find("snake1").GetComponent<Move2>().snakeLength).transform.position;
			GameObject newbody = (GameObject)Instantiate (snakeprefab, NewBodyPosition, Quaternion.identity);
			GameObject.Find("snake1").GetComponent<Move2>().snakeLength++;
			newbody.name = "snake" + GameObject.Find("snake1").GetComponent<Move2>().snakeLength;

			Destroy (this.gameObject);
		}

	}*/
}
