namespace SterlingAssets.VehiclePhysX.Car
{
	public partial class InputProvider
	{
		#region States

		private AbstractState currentState;

		private AbstractState idleState;
		private AbstractState playerControlState;
		private AbstractState botControlState;

		#endregion

		private void MoveToState(AbstractState nextState)
		{
			currentState?.OnExitState();

			currentState = nextState;
			currentState.OnEnterState();
		}
	}
}
