using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public partial class InputProvider : MonoBehaviour
	{
		#region Fields

		private float steerInput;
		private float driveInput;
		private float throttleInput;
		private float brakeInput;
		private float clutchInput;
		private float gearShiftInput;

		#endregion

		#region Getters

		public float SteerInput => steerInput;
		public float DriveInput => driveInput;
		public float ThrottleInput => throttleInput;
		public float BrakeInput => brakeInput;
		public float ClutchInput => clutchInput;
		public float GearShiftInput => gearShiftInput;

		#endregion

		private void Awake()
		{
			idleState = new IdleState(this);
			playerControlState = new PlayerControlState(this);
			botControlState = new BotControlState(this);
		}

		private void Start()
		{
			MoveToState(playerControlState);
		}

		private void Update()
		{
			currentState.OnUpdateState();
		}
	}
}