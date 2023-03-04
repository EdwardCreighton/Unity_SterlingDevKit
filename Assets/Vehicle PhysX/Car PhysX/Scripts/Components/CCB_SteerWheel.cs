using System;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		protected class SteerWheel
		{
			#region Fields

			[SerializeField] private float maxSteerAngle = 50f;
			[SerializeField] private float steerSpeed = 150f;

			private float currentSteerAngle;

			public float CurrentSteerAngle => currentSteerAngle;

			#endregion

			public void ComputeSteer(float steerInput)
			{
				currentSteerAngle = MathE.FixedLerp(steerInput * maxSteerAngle, currentSteerAngle, steerSpeed * Time.deltaTime);
			}
		}
	}
}
