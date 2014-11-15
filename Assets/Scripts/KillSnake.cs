using UnityEngine;
using System.Collections;

// Kills the snake if he collides with poison
public class KillSnake : MonoBehaviour 
{

	GameObject[] snakeBody;

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.name == "Snake") 
		{
			Destroy(GameObject.Find("Snake head"));
			Destroy(GameObject.Find("Snake"));
			snakeBody =  GameObject.FindGameObjectsWithTag("SnakeBody");			
			for(int i = 0; i < snakeBody.Length; i++)
			{
				Destroy(snakeBody[i]);
			}

			GameObject.Find("loserMessage").guiText.enabled = true;
		}
	}
}
