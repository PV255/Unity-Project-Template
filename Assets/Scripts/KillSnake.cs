using UnityEngine;
using System.Collections;

// Kills the snake if he collides with poison
public class KillSnake : MonoBehaviour 
{

	GameObject[] snakeBody;
	public AudioClip glass;
	public bool dead;


	IEnumerator OnTriggerEnter(Collider c)
	{
		if (c.gameObject.name.StartsWith("snake") || (c.gameObject.tag == "Tail")) 
		{

			audio.PlayOneShot (glass);
			GameObject.Find ("loserMessage").guiText.enabled = true;

			(GameObject.Find ("snake1").GetComponent<Move2>()).enabled = false;
			yield return new WaitForSeconds(1);
			GameObject.Find ("_GameManager_").GetComponent<GameManager>().Dead();
			//Destroy (GameObject.Find ("Snake head"));
			snakeBody = GameObject.FindGameObjectsWithTag ("Snake");			
			for (int i = 0; i < snakeBody.Length; i++) 
			{
				Destroy (snakeBody [i]);
			}
			Destroy(this);

		} 
		else 
		{
			yield return new WaitForSeconds(0);
		}
	}
}
