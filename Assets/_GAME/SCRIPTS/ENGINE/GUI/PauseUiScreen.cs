using UnityEngine;
using UnityEngine.UI;
using _GAME.SCRIPTS.TOOLS.PATTERN.STATE;

namespace _GAME.SCRIPTS.ENGINE.GUI
{
	[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(CanvasScaler))]
	public class PauseUiScreen : MonoBehaviour, IState<UiScreen>
	{
		[System.Serializable]
		public class PauseUiComponents : UiComponents
		{

		}

		private const UiScreen STATE_TYPE = UiScreen.PauseUi;
		public UiScreen StateType => STATE_TYPE;
		public bool StateIsActive { get; private set; }
		private GuiManager handler;
		public PauseUiComponents ComponentReferences => componentReferences;
		[SerializeField] private PauseUiComponents componentReferences;

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
