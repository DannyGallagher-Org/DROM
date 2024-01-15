namespace hStates
{
	public interface IState
	{
		void OnExit();
		bool CanTransition();
		IState NextState();
	}
}