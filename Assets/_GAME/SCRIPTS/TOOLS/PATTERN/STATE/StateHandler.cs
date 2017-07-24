using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GAME.SCRIPTS.TOOLS.PATTERN.STATE
{	
	/// <summary>
	/// 
	/// </summary>
	public abstract class StateHandler<T> : MonoBehaviour
	{
		public event System.EventHandler<StateChangedEventArgs<T>> StateChanged;
		protected T CurrentStateType
		{
			get { return currentStateType; }
			set
			{
				if (currentStateType.Equals(value)) return;
				OnStateChanged(new StateChangedEventArgs<T>(newState:value, oldState:currentStateType));
				currentStateType = value;
			}
		}

		private T currentStateType;
		private List<IState<T>> states;

		public void ChangeState(T nextStateType)
		{
			if (currentStateType.Equals(nextStateType)) return;
			GetState(currentStateType).EndState(GetState(nextStateType));
		}

		protected virtual IState<T> GetState(T stateType)
		{
			return states?.Find((state) => state.StateType.Equals(stateType));
		}

		protected virtual void Awake()
		{
			states = InitializeStates();
		}

		protected virtual void Start()
		{
			GetState(SetInitialState()).StartState(currentStateType);
		}
		
		protected abstract List<IState<T>> InitializeStates();
		protected abstract T SetInitialState();

		private void OnStateChanged(StateChangedEventArgs<T> e)
		{
#if DEBUG
			Debug.LogFormat(e.ToString());
#endif
			StateChanged?.Invoke(this, e);
		}
	}

	public struct StateChangedEventArgs<T>
	{
		public readonly T CurrentState;
		public readonly T PreviousState;

		public StateChangedEventArgs(T newState, T oldState)
		{
			CurrentState = newState;
			PreviousState = oldState;
		}

		public override string ToString()
		{
			return string.Format($"[{typeof(T).Name}]\nCURRENT : {CurrentState} , PREVIOUS : {PreviousState}");
		}
	}
}