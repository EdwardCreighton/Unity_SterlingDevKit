namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract class AbstractState
	{
		protected InputProvider controller;

		public AbstractState(InputProvider controller)
		{
			this.controller = controller;
		}
		
		public abstract void OnEnterState();
		public abstract void OnUpdateState();
		public abstract void OnExitState();
	}
}
