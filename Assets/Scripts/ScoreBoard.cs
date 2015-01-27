using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	public int newScore;
	public bool enteringScore = false;
	public string player1;
	public string player2;
	public string player3;
	public string player4;
	public string player5;
	public int score1;
	public int score2;
	public int score3;
	public int score4;
	public int score5;
	public string stringToEdit;
	GUIStyle aFont;
	public int level;

	void Start(){
		//font a tak pro GUI
		aFont = new GUIStyle();
		aFont.fontSize = 56;
		aFont.alignment = TextAnchor.MiddleCenter;
		aFont.normal.textColor = Color.magenta;
		//
		if (GameObject.Find ("_GameManager_") != null) {
			level = GameObject.Find ("_GameManager_").GetComponent<GameManager> ().currentLevel;
			print ("fromgame");
			if(GameObject.Find ("_GameManager_").GetComponent<GameManager>().LastScore() != 0){
				enteringScore = true;
				GameObject.Find ("_GameManager_").GetComponent<GameManager> ().TakeOffPointsAddedAfterSnakeDead (3+level/2);
				newScore = GameObject.Find ("_GameManager_").GetComponent<GameManager>().LastScore();
				print("last score " + newScore);

			}
		}else{
			newScore = 0;
		}
		//nacteni a vypsani score
		PrintScore ();
	}

	void OnGUI() {
		//vypis pokud jde ze hry a zapisuje score
		if(enteringScore == true){
			stringToEdit = GUI.TextField(new Rect(Screen.width/2 -300 , Screen.height /4*3 +20, 200, 20), stringToEdit, 10);
			GUI.Label(new Rect (Screen.width/2 +60, Screen.height /4*3 + 20, 200, 20), newScore.ToString(),aFont);
			//odstraneni tlacitka menu
			GameObject.Find ("Menu").GetComponent<GUITexture>().enabled = false;
			GameObject.Find ("GUI Text").GetComponent<GUIText>().enabled = false;
			//Button for entering score
			if (GUI.Button(new Rect(Screen.width/2 + 250, Screen.height /4*3 + 20, 50, 30), "Enter")){
				FindLowestScore();
				enteringScore = false;
				GameObject.Find ("_GameManager_").GetComponent<GameManager>().EraseScore();
				PrintScore();
				//navraceni tlacitka menu
				GameObject.Find ("Menu").GetComponent<GUITexture>().enabled = true;
				GameObject.Find ("GUI Text").GetComponent<GUIText>().enabled = true;
			}
		}
	}

	void FindLowestScore(){
		if (score1 <= newScore) {
			//score1 = newscore....
			score5 = score4;
			player5 = player4;
			score4 = score3;
			player4 = player3;
			score3 = score2;
			player3 = player2;
			score2 = score1;
			player2 = player1;
			score1 = newScore;
			player1 = stringToEdit;
		}else
		{
			if(score2 <= newScore){
				//score2 = newscore...
				score5 = score4;
				player5 = player4;
				score4 = score3;
				player4 = player3;
				score3 = score2;
				player3 = player2;
				score2 = newScore;
				player2 = stringToEdit;
			}else
			{
				if (score3 <= newScore) {
					//score3 = newscore
					score5 = score4;
					player5 = player4;
					score4 = score3;
					player4 = player3;
					score3 = newScore;
					player3 = stringToEdit;
				} else 
				{
					if (score4 <= newScore) {
						//score4 = newscore ...
						score5 = score4;
						player5 = player4;
						score4 = newScore;
						player4 = stringToEdit;
					} else {
							//score5 = newscore plazer5 = "new palzer"
							score5 = newScore;
							player5 = stringToEdit;
						}
					}		
				}
			}
		SaveScore ();
	}

	void PrintScore(){
		LoadScore ();
		this.guiText.text = player1 + "       " + score1 + "\n" + player2 + "       " + score2 + "\n" + player3 + "       " + score3 + "\n" + player4 + "       " + score4 + "\n" + player5 + "       " + score5 + "\n";
	}

	void SaveScore(){
		PlayerPrefs.SetString ("Name1", player1);
		PlayerPrefs.SetInt ("Score1", score1);
		PlayerPrefs.SetString ("Name2", player2);
		PlayerPrefs.SetInt ("Score2", score2);
		PlayerPrefs.SetString ("Name3", player3);
		PlayerPrefs.SetInt ("Score3", score3);
		PlayerPrefs.SetString ("Name4", player4);
		PlayerPrefs.SetInt ("Score4", score4);
		PlayerPrefs.SetString ("Name5", player5);
		PlayerPrefs.SetInt ("Score5", score5);
	}

	void LoadScore(){
		player1 = PlayerPrefs.GetString ("Name1");
		score1 = PlayerPrefs.GetInt ("Score1");
		player2 = PlayerPrefs.GetString ("Name2");
		score2 = PlayerPrefs.GetInt ("Score2");
		player3 = PlayerPrefs.GetString ("Name3");
		score3 = PlayerPrefs.GetInt ("Score3");
		player4 = PlayerPrefs.GetString ("Name4");
		score4 = PlayerPrefs.GetInt ("Score4");
		player5 = PlayerPrefs.GetString ("Name5");
		score5 = PlayerPrefs.GetInt ("Score5");
	}

	void EraseScore(){
		player1 = "";
		player2 = "";
		player3 = "";
		player4 = "";
		player5 = "";
		score1 = 0;
		score2 = 0;
		score3 = 0;
		score4 = 0;
		score5 = 0;
		SaveScore ();
	}

}
