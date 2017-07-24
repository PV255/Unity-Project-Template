using UnityEngine;
using UnityEngine.UI;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GUI
{
	[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(CanvasScaler))]
	public class GameOverUiScreen : MonoBehaviour, IState<UiScreen>
	{
		[System.Serializable]
		public class GameOverUiComponents : UiComponents
		{

		}

		private const UiScreen STATE_TYPE = UiScreen.GameOverUi;
		public UiScreen StateType => STATE_TYPE;
		public bool StateIsActive { get; private set; }
		private GuiManager handler;
		public GameOverUiComponents ComponentReferences => componentReferences;
		[SerializeField] private GameOverUiComponents componentReferences;

		public void InitializeState(GuiManager handler)
		{
			this.handler = handler;
		}

		public void StartState(UiScreen previousStateType)
		{
			StateIsActive = true;
			handler.StartCoroutine(handler.ScreenStarting(this, previousStateType));
		}

		public void EndState(IState<UiScreen> nextState)
		{
			StateIsActive = false;
			handler.StartCoroutine(handler.ScreenEnding(this, nextState));
		}
	}
}
