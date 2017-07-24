using UnityEngine;
using UnityEngine.UI;

namespace _GAME.SCRIPTS.ENGINE.GUI
{
	[System.Serializable]
	public abstract class UiComponents
	{
		public Canvas Canvas => canvas;
		public CanvasGroup CanvasGroup => canvasGroup;
		public CanvasScaler CanvasScaler => canvasScaler;
		[SerializeField] private Canvas canvas;
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private CanvasScaler canvasScaler;
	}
}
