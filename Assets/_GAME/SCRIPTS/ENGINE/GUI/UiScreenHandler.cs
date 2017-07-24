using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using _GAME.SCRIPTS.ENGINE.GAMEPLAY;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GUI
{	
	/// <summary>
	/// 
	/// </summary>
	public abstract class UiScreenHandler : StateHandler<UiScreen>
	{
		public IEnumerator ScreenStarting(SplashUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenStarting(MainMenuUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenStarting(GamePlayUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenStarting(PauseUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenStarting(LevelCompleteUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenStarting(GameOverUiScreen screen, UiScreen previousScreenType)
		{
			CurrentStateType = screen.StateType;
			StartCoroutine(OnScreenStart(screen, previousScreenType));
			do yield return StartCoroutine(OnScreenStay(screen)); while (screen.StateIsActive);
		}

		public IEnumerator ScreenEnding(SplashUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		public IEnumerator ScreenEnding(MainMenuUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		public IEnumerator ScreenEnding(GamePlayUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		public IEnumerator ScreenEnding(PauseUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		public IEnumerator ScreenEnding(LevelCompleteUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		public IEnumerator ScreenEnding(GameOverUiScreen screen, IState<UiScreen> nextScreen)
		{
			yield return StartCoroutine(OnScreenEnd(screen, nextScreen.StateType));
			nextScreen.StartState(screen.StateType);
		}

		protected abstract IEnumerator OnScreenStart(SplashUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStart(MainMenuUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStart(GamePlayUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStart(PauseUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStart(LevelCompleteUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStart(GameOverUiScreen screen, UiScreen previousScreenType);
		protected abstract IEnumerator OnScreenStay(SplashUiScreen screen);
		protected abstract IEnumerator OnScreenStay(MainMenuUiScreen screen);
		protected abstract IEnumerator OnScreenStay(GamePlayUiScreen screen);
		protected abstract IEnumerator OnScreenStay(PauseUiScreen screen);
		protected abstract IEnumerator OnScreenStay(LevelCompleteUiScreen screen);
		protected abstract IEnumerator OnScreenStay(GameOverUiScreen screen);
		protected abstract IEnumerator OnScreenEnd(SplashUiScreen screen, UiScreen nextScreenStateType);
		protected abstract IEnumerator OnScreenEnd(MainMenuUiScreen screen, UiScreen nextScreenStateType);
		protected abstract IEnumerator OnScreenEnd(GamePlayUiScreen screen, UiScreen nextScreenStateType);
		protected abstract IEnumerator OnScreenEnd(PauseUiScreen screen, UiScreen nextScreenStateType);
		protected abstract IEnumerator OnScreenEnd(LevelCompleteUiScreen screen, UiScreen nextScreenStateType);
		protected abstract IEnumerator OnScreenEnd(GameOverUiScreen screen, UiScreen nextScreenStateType);
	}
}