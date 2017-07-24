using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GAMEPLAY
{
	public abstract class GameStateHandler : StateHandler<GameState>
	{
		public IEnumerator StateStarting(PreloadGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(SplashGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(MainMenuGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(GameStartGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(LevelStartGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(LevelPlayingGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(LevelCompleteGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(DeathGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateStarting(RestartGameState state, GameState previousStateType)
		{
			CurrentStateType = state.StateType;
			StartCoroutine(OnStateStart(state, previousStateType));
			do yield return StartCoroutine(OnStateStay(state)); while (state.StateIsActive);
		}

		public IEnumerator StateEnding(PreloadGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}
		
		public IEnumerator StateEnding(SplashGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(MainMenuGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(GameStartGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(LevelStartGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(LevelPlayingGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(LevelCompleteGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(DeathGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		public IEnumerator StateEnding(RestartGameState state, IState<GameState> nextState)
		{
			yield return StartCoroutine(OnStateEnd(state, nextState.StateType));
			nextState.StartState(state.StateType);
		}

		protected abstract IEnumerator OnStateStart(PreloadGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(SplashGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(MainMenuGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(GameStartGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(LevelStartGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(LevelPlayingGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(LevelCompleteGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(DeathGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStart(RestartGameState state, GameState previousStateType);
		protected abstract IEnumerator OnStateStay(PreloadGameState state);
		protected abstract IEnumerator OnStateStay(SplashGameState state);
		protected abstract IEnumerator OnStateStay(MainMenuGameState state);
		protected abstract IEnumerator OnStateStay(GameStartGameState state);
		protected abstract IEnumerator OnStateStay(LevelStartGameState state);
		protected abstract IEnumerator OnStateStay(LevelPlayingGameState state);
		protected abstract IEnumerator OnStateStay(LevelCompleteGameState state);
		protected abstract IEnumerator OnStateStay(DeathGameState state);
		protected abstract IEnumerator OnStateStay(RestartGameState state);
		protected abstract IEnumerator OnStateEnd(PreloadGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(SplashGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(MainMenuGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(GameStartGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(LevelStartGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(LevelPlayingGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(LevelCompleteGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(DeathGameState state, GameState nextStateStateType);
		protected abstract IEnumerator OnStateEnd(RestartGameState state, GameState nextStateStateType);

		
	}
}
