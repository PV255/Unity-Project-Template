using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using _GAME.SCRIPTS.ENGINE.GUI;
using _GAME.SCRIPTS.TOOLS.PATTERN.SINGLETON;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;
using Object = UnityEngine.Object;

namespace _GAME.SCRIPTS.ENGINE.GAMEPLAY
{
	public enum GameState
	{
		Preload,
		Splash,
		MainMenu,
		GameStart,
		LevelStart,
		LevelPlaying,
		LevelComplete,
		Death,
		Restart
	}

	/// <summary>
	/// singleton that handles game state machine
	/// </summary>
	public class GameController : GameStateHandler
	{
		[System.Serializable]
		public class ComponentRefererences
		{
			public Camera MainCamera
			{
				get { return mainCamera; }
				set
				{
					OnUiCameraChanged(value);
					mainCamera = value;
				}
			}

			public Camera UiCamera
			{
				get { return uiCamera; }
				set
				{
					OnMainCameraChanged(value);
					uiCamera = value;
				}
			}

			public StateHandler<UiScreen> UiScreenHandler
			{
				get { return uiScreenHandler; }
				set
				{
					OnUiScreenHandlerChanged(value);
					uiScreenHandler = value;
				}
			}
			
			private Camera uiCamera, mainCamera;
			private StateHandler<UiScreen> uiScreenHandler;

			private static void OnMainCameraChanged(Camera obj)
			{
				Singleton<GameController>.Instance.MainCameraChanged?.Invoke(obj);
			}

			private static void OnUiCameraChanged(Camera obj)
			{
				Singleton<GameController>.Instance.UiCameraChanged?.Invoke(obj);
			}

			private static void OnUiScreenHandlerChanged(StateHandler<UiScreen> obj)
			{
				Singleton<GameController>.Instance.UiScreenHandlerChanged?.Invoke(obj);
			}
		}

		public const string
			PRELOAD_SCENE_NAME = "Preload",
			SPLASH_SCENE_NAME = "Splash",
			MAIN_MENU_SCENE_NAME = "Main Menu",
			GAME_SCENE_NAME = "Game",
			ERROR_SCENE_NAME_NOT_DEFINED = "The active scene name is not defined as a string.";

		public event System.Action<Camera> MainCameraChanged, UiCameraChanged;
		public event System.Action<StateHandler<UiScreen>> UiScreenHandlerChanged;
		public ComponentRefererences References { get; set; } = new ComponentRefererences();
		public PreloadGameStateComponents PreloadComponents => preloadComponents;

		[SerializeField] private PreloadGameStateComponents preloadComponents;
		private static Singleton<GameController> singleton;

		protected override List<IState<GameState>> InitializeStates()
		{
			return new List<IState<GameState>>()
			{
				new PreloadGameState(this),
				new SplashGameState(this),
				new MainMenuGameState(this),
				new GameStartGameState(this),
				new LevelStartGameState(this),
				new LevelPlayingGameState(this),
				new LevelCompleteGameState(this),
				new DeathGameState(this),
				new RestartGameState(this),
			};
		}

		protected override GameState SetInitialState()
		{
			return GameState.Preload;
		}

		protected override void Awake()
		{
			singleton = new Singleton<GameController>(this, true);
			base.Awake();
		}

		#region PRELOAD

		[System.Serializable]
		public class PreloadGameStateComponents
		{
			[SerializeField] private Camera mainCameraPrefab, uiCameraPrefab;
			[SerializeField] private GuiManager guiManagerPrefab;

			public void LoadAssets()
			{
				LoadCameras();
				LoadGui();
			}

			#region Private
			
			private void LoadCameras()
			{
				CheckForExistingCamerasInScene();
				if (!Singleton<GameController>.Instance.References.MainCamera) AttachNewMainCamera();
				if (!Singleton<GameController>.Instance.References.UiCamera) AttachNewUiCamera();
			}

			private void LoadGui()
			{
				CheckForExistingGuiInScene();
				if(!Singleton<GameController>.Instance.References.UiScreenHandler)
					Singleton<GameController>.Instance.References.UiScreenHandler = Instantiate(guiManagerPrefab, Singleton<GameController>.Instance.transform);
			}

			private static void CheckForExistingGuiInScene()
			{
				if (!FindObjectOfType<GuiManager>()) return;
				Singleton<GameController>.Instance.References.UiScreenHandler = FindObjectOfType<GuiManager>();
				Singleton<GameController>.Instance.References.UiScreenHandler.transform.SetParent(Singleton<GameController>
					.Instance.transform);
			}

			private static void CheckForExistingCamerasInScene()
			{
				foreach (var camera in Camera.allCameras)
				{
					if (camera.CompareTag("MainCamera")) AttachExistingMainCamera(camera);
					else if (camera.CompareTag("UiCamera")) AttachExistingUiCamera(camera);
					else Object.Destroy(camera.gameObject);
				}
			}

			private static void AttachExistingMainCamera(Camera camera)
			{
				Singleton<GameController>.Instance.References.MainCamera = camera;
				Singleton<GameController>.Instance.References.MainCamera.transform.SetParent(Singleton<GameController>.Instance.transform);
			}

			private static void AttachExistingUiCamera(Camera camera)
			{
				Singleton<GameController>.Instance.References.UiCamera = camera;
				Singleton<GameController>.Instance.References.UiCamera.transform.SetParent(Singleton<GameController>.Instance.transform);
			}

			private void AttachNewMainCamera()
			{
				Singleton<GameController>.Instance.References.MainCamera =
					Object.Instantiate(mainCameraPrefab, Singleton<GameController>.Instance.transform);
			}

			private void AttachNewUiCamera()
			{
				Singleton<GameController>.Instance.References.UiCamera =
					Object.Instantiate(uiCameraPrefab, Singleton<GameController>.Instance.transform);
			}

			#endregion

		}

		protected override IEnumerator OnStateStart(PreloadGameState state, GameState previousStateType)
		{
			yield return StartCoroutine(state.LoadPreloadAssets());
			yield return StartCoroutine(state.SetFirstStateBasedOnActiveScene());
		}

		protected override IEnumerator OnStateStay(PreloadGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(PreloadGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}
		
		#endregion

		#region SPLASH

		protected override IEnumerator OnStateStart(SplashGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(SplashGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(SplashGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region MAIN_MENU
		
		protected override IEnumerator OnStateStart(MainMenuGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(MainMenuGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(MainMenuGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region GAME_START
		
		protected override IEnumerator OnStateStart(GameStartGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(GameStartGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(GameStartGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region LEVEL_START
		
		protected override IEnumerator OnStateStart(LevelStartGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(LevelStartGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(LevelStartGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region LEVEL_PLAYING
		
		protected override IEnumerator OnStateStart(LevelPlayingGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(LevelPlayingGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(LevelPlayingGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region LEVEL_COMPLETE
		
		protected override IEnumerator OnStateStart(LevelCompleteGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(LevelCompleteGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(LevelCompleteGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region DEATH
		
		protected override IEnumerator OnStateStart(DeathGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(DeathGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(DeathGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion

		#region RESTART
		
		protected override IEnumerator OnStateStart(RestartGameState state, GameState previousStateType)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateStay(RestartGameState state)
		{
			yield return 0;
		}

		protected override IEnumerator OnStateEnd(RestartGameState state, GameState nextStateStateType)
		{
			yield return 0;
		}

		#endregion
		
	}
}