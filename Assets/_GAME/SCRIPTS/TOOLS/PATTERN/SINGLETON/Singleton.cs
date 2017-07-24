using UnityEngine;

namespace _GAME.SCRIPTS.TOOLS.PATTERN.SINGLETON
{
	/// <summary>
	/// Creates a Singleton instance.
	/// </summary>
	public class Singleton<T> where T: MonoBehaviour
	{
		public static T Instance { get; private set; }

		public Singleton(T monoBehaviour, bool isPersistentSingleton)
		{
			if(Instance == null) SetInstance(monoBehaviour, isPersistentSingleton);
			else Object.DestroyImmediate(monoBehaviour.gameObject);
		}

		private static void SetInstance(MonoBehaviour monoBehaviour, bool isPersistentSingleton)
		{
			if(isPersistentSingleton) Object.DontDestroyOnLoad(monoBehaviour.gameObject);
			Instance = monoBehaviour as T;
		}
	}
}