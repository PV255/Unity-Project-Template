using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour 
{
	public GameObject food;
	public GameObject snakeprefab;
	public GameObject head;
	public GameObject parent;
	private bool alive = true;
	public AudioClip eating;
	public int score;

	void Start()
	{
		head = GameObject.Find ("Snake");
		}
	// Use this for initialization
	void OnTriggerEnter(Collider c)
	{
		if (alive) 
		{
			audio.PlayOneShot(eating);
			//yield return new WaitForSeconds(2);
			alive = false;
			print ("colliding");
			Destroy (this.gameObject);


			float px = (float)Random.value * 10 - 0.5f;
			float py = (float)Random.value * 10 - 9.5f;
			float pz = (float)Random.value * 10 - 4.5f;
			Vector3 foodPosition = new Vector3 (px, py, pz);
			Instantiate (food, foodPosition, Quaternion.identity);
			print ("created food");
			//pozicia hada - buduca pozicia dalsieho kuska tela
			WhereIAM pos = head.GetComponent <WhereIAM> ();
			Vector3 NewBodyPosition = pos.returnPreviousPosition ();
			GameObject newbody = (GameObject)Instantiate (snakeprefab, NewBodyPosition, Quaternion.identity);
			newbody.transform.parent = FindSon (head.transform);




		/*	print ("eaten food");
			score = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().LastScore();
			print("level "+ score);

			//uspesne ukonceny level - had zjedol 5 potrav
			if ((score%5) == 0)
			{
				print("won the level");
				Application.LoadLevel("WinningScreen");
			}*/
		}

	}
	public Transform FindSon(Transform oldParent)
	{



			if(transform.childCount > 0) {
						Transform son = oldParent.transform.GetChild (1);
						return FindSon (son);
						
				} else {
						return oldParent.transform;
				}
		}
}
