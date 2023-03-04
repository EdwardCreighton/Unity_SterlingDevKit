using System;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		public class Clutch
		{
			#region Fields

			[SerializeField] private float clutchStiffness = 20f;
			[SerializeField] private float clutchCapacity = 1.3f;
			[SerializeField] private float maxEngineTorque = 400f;
			[SerializeField] private float minFriction = 0.1f;
			[SerializeField] private float frictionForce = 0.02f;

			private float effectiveTorque;
			private float loadTorque;

			private float currentAngularVelocity;

			#endregion

			#region Getters

			public float CurrentAngularVelocity => currentAngularVelocity;
			public float EffectiveTorque => effectiveTorque;
			public float LoadTorque => loadTorque;

			#endregion

			public void TorqueFlow(float engineAngVel, float shaftVelocity, float gearRatio, float clutchInput)
			{
				float angularSlip = engineAngVel - shaftVelocity;
				angularSlip *= Mathf.Abs(MathE.Sign(gearRatio));

				float tempTorque = angularSlip * clutchStiffness * (1 - clutchInput);

				float maxTorque = maxEngineTorque * clutchCapacity;
				tempTorque = Mathf.Clamp(tempTorque, -maxTorque, maxTorque);

				float damping = (effectiveTorque - tempTorque) * 0.7f;
				effectiveTorque = tempTorque + damping;
			}

			public void VelocityFlow(float engineAngVel, float shaftAngularVelocity, float gearRatio, float clutchInput)
			{
				if (Mathf.Abs(MathE.Sign(gearRatio)) < 1)
				{
					// skip
				}
				else
				{
					currentAngularVelocity = shaftAngularVelocity;
				}
				
				float angularSlip = engineAngVel - currentAngularVelocity;
				loadTorque = angularSlip * (1 - clutchInput) * clutchStiffness;
			}
		}
	}
}
