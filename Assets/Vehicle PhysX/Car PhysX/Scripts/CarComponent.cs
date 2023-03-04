using SterlingTools;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public class CarComponent : CarComponentBase<WheelComponent>
	{
		#region Fields

		private InputProvider inputProvider;

		#endregion

		private void Awake()
		{
			inputProvider = GetComponent<InputProvider>();
			
			SetCenterOfMass();
		}

		private void Update()
		{
			steerWheel.ComputeSteer(inputProvider.SteerInput);
			
			drivetrain.Update(steerWheel.CurrentSteerAngle, inputProvider.GearShiftInput);
		}

		private void FixedUpdate()
		{
			drivetrain.FixedUpdate(inputProvider.ThrottleInput, inputProvider.ClutchInput, inputProvider.BrakeInput);
		}

		public void GetControlData(ControlData controlData)
		{
			drivetrain.GetControlData(controlData);

			Vector3 velocityLocal = transform.InverseTransformDirection(rigidbody.velocity);
			controlData.carSpeed = Mathf.Abs(velocityLocal.z * MathE.Mps2Kph);
		}

		[ContextMenu("Create Car")]
		private new void CreateCar()
		{
			base.CreateCar();
		}
	}
}
