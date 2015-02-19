using UnityEngine;
using System.Collections;

// kills the snake if he collides with poison
public class KillSnake : MonoBehaviour 
{

	private GameObject[] snakeBody;
	public AudioClip death;
	public bool dead;


	IEnumerator OnTriggerEnter(Collider c)
	{
		if (c.gameObject.name.StartsWith("snake") || (c.gameObject.tag == "Tail")) 
		{

			audio.PlayOneShot (death);
			GameObject.Find ("loserMessage").guiText.enabled = true;

			(GameObject.Find ("snake1").GetComponent<Move2>()).enabled = false;
			yield return new WaitForSeconds(3);
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
			Destroy(this);

		} 
		else 
		{
			yield return new WaitForSeconds(0);
		}
	}
}
