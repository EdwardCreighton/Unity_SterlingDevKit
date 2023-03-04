using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public partial class InputProvider
	{
		public class PlayerControlState : AbstractState
		{
			public PlayerControlState(InputProvider controller) : base(controller)
			{
				
			}
			
			public override void OnEnterState()
			{
				
			}

			public override void OnUpdateState()
			{
				controller.steerInput = SterlingInputManager.Instance.CarActions.Steer.ReadValue<float>();
				controller.driveInput = SterlingInputManager.Instance.CarActions.Drive.ReadValue<float>();

				controller.throttleInput = Mathf.Clamp(controller.driveInput, 0f, 1f);
				controller.brakeInput = Mathf.Abs(Mathf.Clamp(controller.driveInput, -1f, 0f));
				
				controller.clutchInput = SterlingInputManager.Instance.CarActions.Clutch.ReadValue<float>();
				controller.gearShiftInput = SterlingInputManager.Instance.CarActions.GearShift.ReadValue<float>();
			}

			public override void OnExitState()
			{
				
			}
		}
	}
}
