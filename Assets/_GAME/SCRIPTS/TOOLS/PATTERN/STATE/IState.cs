namespace _GAME.SCRIPTS.TOOLS.PATTERN.STATE
{
	/// <summary>
	/// 
	/// </summary>
	public interface IState<T>
	{
		T StateType { get; }
		bool StateIsActive { get; }
		void StartState(T previousStateType);
		void EndState(IState<T> nextState);
	}
}