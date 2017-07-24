using UnityEngine;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GAMEPLAY
{
	[System.Serializable]
	public class MainMenuGameState : IState<GameState>
	{
		private const GameState STATE_TYPE = GameState.MainMenu;
		public GameState StateType => STATE_TYPE;
		private bool stateIsActive;
		private readonly GameController handler;

		public bool StateIsActive
		{
			get { return stateIsActive; }
			set { stateIsActive = value; }
		}

		public MainMenuGameState(GameController handler)
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
	}
}
