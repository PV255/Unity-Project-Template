using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using _GAME.SCRIPTS.TOOLS.PATTERN.SINGLETON;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GAMEPLAY
{
	[System.Serializable]
	public class PreloadGameState : IState<GameState>
	{
		private const GameState STATE_TYPE = GameState.Preload;
		public GameState StateType => STATE_TYPE;
		private bool stateIsActive;
		private readonly GameController handler;

		public bool StateIsActive
		{
			get { return stateIsActive; }
			set { stateIsActive = value; }
		}

		public PreloadGameState(GameController handler)
		{
			this.handler = handler;
		}

		public void StartState(GameState previousStateType)
		{
			StateIsActive = true;
			handler.StartCoroutine(handler.StateStarting(this, previousStateType));
		}

		public void EndState(IState<GameState> nextState)
		{
			stateIsActive = false;
			handler.StartCoroutine(handler.StateEnding(this, nextState));
		}

		public IEnumerator LoadPreloadAssets()
		{
			handler.PreloadComponents.LoadAssets();
			System.Func<bool> preloadAssetsHaveLoaded = () => Singleton<GameController>.Instance.References.MainCamera && Singleton<GameController>.Instance.References.UiCamera;
			yield return new WaitUntil(preloadAssetsHaveLoaded);
		}

		public IEnumerator SetFirstStateBasedOnActiveScene()
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case GameController.PRELOAD_SCENE_NAME:
					SceneManager.LoadScene(GameController.SPLASH_SCENE_NAME);
					System.Func<bool> splashSceneHasLoaded = () => SceneManager.GetActiveScene().name == GameController.SPLASH_SCENE_NAME;
					yield return new WaitUntil(splashSceneHasLoaded);
					Singleton<GameController>.Instance.ChangeState(GameState.Splash);
					break;
				case GameController.SPLASH_SCENE_NAME:
					Singleton<GameController>.Instance.ChangeState(GameState.Splash);
					break;
				case GameController.MAIN_MENU_SCENE_NAME:
					Singleton<GameController>.Instance.ChangeState(GameState.MainMenu);
					break;
				case GameController.GAME_SCENE_NAME:
					Singleton<GameController>.Instance.ChangeState(GameState.GameStart);
					break;
				default:
					throw new UnityException(GameController.ERROR_SCENE_NAME_NOT_DEFINED);
			}
		}
	}
}
