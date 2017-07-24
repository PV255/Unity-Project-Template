using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using _GAME.SCRIPTS.ENGINE.GAMEPLAY;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GUI
{
	public enum UiScreen
	{
		SplashUi,
		MainMenuUi,
		GamePlayUi,
		PauseUi,
		LevelCompleteUi,
		GameOverUi
	}

	/// <summary>
	/// 
	/// </summary>
	public class GuiManager : UiScreenHandler 
	{
		[System.Serializable]
		protected class UiScreenAssets
		{
			public List<IState<UiScreen>> UiScreens { get; } = new List<IState<UiScreen>>();
			[SerializeField] private SplashUiScreen splashUiScreen;
			[SerializeField] private MainMenuUiScreen mainMenuUiScreen;
			[SerializeField] private GamePlayUiScreen gamePlayUiScreen;
			[SerializeField] private PauseUiScreen pauseUiScreen;
			[SerializeField] private LevelCompleteUiScreen levelCompleteUiScreen;
			[SerializeField] private GameOverUiScreen gameOverUiScreen;

			public void LoadAssets(GuiManager handler)
			{
				InitializeSplashUi(handler);
				InitializeMainMenuUi(handler);
				InitializeGamePlayUi(handler);
				InitializePauseUi(handler);
				InitializeLevelCompleteUi(handler);
				InitializeGameOverUi(handler);
			}

			#region Private
			
			private void InitializeSplashUi(GuiManager handler)
			{
				var splashInstance = Instantiate(splashUiScreen, handler.transform);
				splashInstance.InitializeState(handler);
				UiScreens.Add(splashInstance);
			}

			private void InitializeMainMenuUi(GuiManager handler)
			{
				var mainMenuInstance = Instantiate(mainMenuUiScreen, handler.transform);
				mainMenuInstance.InitializeState(handler);
				UiScreens.Add(mainMenuInstance);
			}

			private void InitializeGamePlayUi(GuiManager handler)
			{
				var gamePlayInstance = Instantiate(gamePlayUiScreen, handler.transform);
				gamePlayInstance.InitializeState(handler);
				UiScreens.Add(gamePlayInstance);
			}

			private void InitializePauseUi(GuiManager handler)
			{
				var pauseInstance = Instantiate(pauseUiScreen, handler.transform);
				pauseInstance.InitializeState(handler);
				UiScreens.Add(pauseInstance);
			}

			private void InitializeLevelCompleteUi(GuiManager handler)
			{
				var levelCompleteInstance = Instantiate(levelCompleteUiScreen, handler.transform);
				levelCompleteInstance.InitializeState(handler);
				UiScreens.Add(levelCompleteInstance);
			}

			private void InitializeGameOverUi(GuiManager handler)
			{
				var gameOverInstance = Instantiate(gameOverUiScreen, handler.transform);
				gameOverInstance.InitializeState(handler);
				UiScreens.Add(gameOverInstance);
			}


			#endregion

		}

		[SerializeField] private UiScreenAssets assetsToLoad;

		protected override List<IState<UiScreen>> InitializeStates()
		{
			assetsToLoad.LoadAssets(this);
			return assetsToLoad.UiScreens;
		}

		protected override UiScreen SetInitialState()
		{
			return SetInitialStateBasedOnActiveScene();
		}

		#region SPLASH_UI
		
		protected override IEnumerator OnScreenStart(SplashUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}
		
		protected override IEnumerator OnScreenStay(SplashUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(SplashUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}
		
		#endregion

		#region MAIN_MENU_UI
		
		protected override IEnumerator OnScreenStart(MainMenuUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenStay(MainMenuUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(MainMenuUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}

		#endregion

		#region GAME_PLAY_UI

		protected override IEnumerator OnScreenStart(GamePlayUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenStay(GamePlayUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(GamePlayUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}

		#endregion

		#region PAUSE_UI

		protected override IEnumerator OnScreenStart(PauseUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenStay(PauseUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(PauseUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}

		#endregion

		#region LEVEL_COMPLETE_UI

		protected override IEnumerator OnScreenStart(LevelCompleteUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenStay(LevelCompleteUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(LevelCompleteUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}

		#endregion

		#region GAME_OVER_UI

		protected override IEnumerator OnScreenStart(GameOverUiScreen screen, UiScreen previousScreenType)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenStay(GameOverUiScreen screen)
		{
			yield return 0;
		}

		protected override IEnumerator OnScreenEnd(GameOverUiScreen screen, UiScreen nextScreenStateType)
		{
			yield return 0;
		}

		#endregion

		private static UiScreen SetInitialStateBasedOnActiveScene()
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case GameController.PRELOAD_SCENE_NAME:
				case GameController.SPLASH_SCENE_NAME:
					return UiScreen.SplashUi;
				case GameController.MAIN_MENU_SCENE_NAME:
					return UiScreen.MainMenuUi;
				case GameController.GAME_SCENE_NAME:
					return UiScreen.GamePlayUi;
				default:
					throw new UnityException(GameController.ERROR_SCENE_NAME_NOT_DEFINED);
			}
		}
		
	}
}