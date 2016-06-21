using UnityEngine;
using System.Collections;

using SimpleJSON;
using System.IO;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;

namespace SaveLoad
{
	public static class Loading {

		private static JSONNode levels;


		private static int lastLevelId = -1;
		private static bool[] unlockLevels;
		private static int[] highscoreLevels;

		public static JSONNode LoadLevels()
		{
			if (lastLevelId != -1) {
				return levels;
			}

			Debug.Log ("LoadLevels()");
			try
			{
				TextAsset data = Resources.Load("TextAsset/test") as TextAsset;
				levels = JSON.Parse(data.text);

				lastLevelId = 0;
				unlockLevels = new bool[levels["levels"].Count];
				unlockLevels[0] = true;
				highscoreLevels = new int[levels["levels"].Count];
				highscoreLevels[0] = 0;

				for (int i=1; i<levels["levels"].Count; i++)
				{
					unlockLevels[i] = false;
					highscoreLevels[i] = 0;
				}

				LoadGame();

				return levels;
			}
			catch
			{
				Debug.Log("failed load levels");
				return null;
			}
		}

		public static JSONNode GetBariers(int levelId)
		{
			if(levels["levels"].Count > levelId)
			{
				return levels ["levels"] [levelId] ["bariers"];
			}

			return null;
		}

		public static JSONNode GetPictures(int levelId)
		{
			if(levels["levels"].Count > levelId)
			{
				return levels ["levels"] [levelId] ["pictures"];
			}

			return null;
		}

		public static int getHighscore(int levelId)
		{
			Debug.Log ("get highscore" + highscoreLevels [levelId]);
			return highscoreLevels[levelId];
		}

		public static bool setScore(int levelId, int score)
		{
			if (levelId >= highscoreLevels.Length) {
				return false;
			}

			if (score > highscoreLevels [levelId]) {
				highscoreLevels [levelId] = score;
				Debug.Log ("set highscore"+ highscoreLevels [levelId]);
				return true;
			}

			return false;
		}

		public static bool isLock(int levelId)
		{
			if (levelId >= unlockLevels.Length) {
				Debug.Log ("wrong id" + levelId);
				return true;
			}

			return !(unlockLevels [levelId]);
		}

		public static void unlockLevel(int levelId)
		{
			unlockLevels [levelId] = true;
		}

		public static int getLastLevelId()
		{
			return lastLevelId;
		}

		public static void setLastLevelId(int id)
		{
			lastLevelId = id;
		}

		public static void resetHighscore()
		{
			for (int i=0; i<levels["levels"].Count; i++)
			{
				highscoreLevels[i] = 0;
			}
		}

		private static bool Save()
		{

			return true;
		}
	
		public static void SaveGame()
		{
			Debug.Log ("SaveGame");
			FileStream file = File.Create(Application.persistentDataPath + "save.snck");
			BinaryFormatter bf = new BinaryFormatter ();
			bf.Serialize (file, lastLevelId);
			bf.Serialize (file, unlockLevels);
			bf.Serialize (file, highscoreLevels);
			file.Close ();
		}

		public static void LoadGame()
		{
			if(File.Exists(Application.persistentDataPath + "save.snck"))
			{
				Debug.Log ("LoadGame");
				FileStream file = File.Open(Application.persistentDataPath + "save.snck", FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter();
				lastLevelId = (int)bf.Deserialize(file);
				unlockLevels = (bool[])bf.Deserialize(file);
				highscoreLevels = (int[])bf.Deserialize(file);
				file.Close();
			}
		}
	}
}
