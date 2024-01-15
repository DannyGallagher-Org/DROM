namespace hStates
{
	public interface IPausable 
	{
		void ApplicationHasGoneIntoBackground();
		void ApplicationHasGoneIntoForeground();
	}
}