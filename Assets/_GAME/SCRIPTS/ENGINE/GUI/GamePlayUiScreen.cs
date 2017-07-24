using UnityEngine;
using UnityEngine.UI;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GUI
{
	[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(CanvasScaler))]
	public class GamePlayUiScreen : MonoBehaviour, IState<UiScreen>
	{
		[System.Serializable]
		public class GamePlayUiComponents : UiComponents
		{

		}

		private const UiScreen STATE_TYPE = UiScreen.GamePlayUi;
		public UiScreen StateType => STATE_TYPE;
		public bool StateIsActive { get; private set; }
		private GuiManager handler;
		public GamePlayUiComponents ComponentReferences => componentReferences;
		[SerializeField] private GamePlayUiComponents componentReferences;
		
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
