using System;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		private class Engine
		{
			#region Fields

			[SerializeField] private AnimationCurve torqueCurve = new AnimationCurve()
			{
				keys = new Keyframe[]
				{
					new Keyframe(0f, 0f, 0f, 0.2f),
					new Keyframe(4000f, 450f),
					new Keyframe(10000f, 0f, -0.2f, 0f)
				}
			};
			[SerializeField] private float minRpm = 1000f;
			[SerializeField] private float maxRpm = 8000f;

			[SerializeField] private float inertia = 0.2f;
			
			[SerializeField] private float minTorqueFriction = 45f;
			[SerializeField] private float torqueFrictionForce = 0.02f;

			[SerializeField] private PD_Controller throttleHandler;
			
			private float throttle;

			private float effectiveTorque;
			private float currentAngularVelocity = 104f;
			private float currentRpm = 1000f;

			#endregion

			#region Getters

			public float Throttle => throttle;
			public float CurrentRpm => currentRpm;
			public float MaxRpm => maxRpm;
			public float CurrentAngularVelocity => currentAngularVelocity;
			public float EffectiveTorque => effectiveTorque;

			#endregion

			public void TorqueFlow(float throttleInput, float loadTorque)
			{
				/*if (currentRpm <= minRpm && controlInput <= 0.01f)
				{
					throttle = throttleHandler.GetAdjustedValueNorm(currentRpm, minRpm, Time.fixedDeltaTime);
					throttle = Mathf.Max(throttle, 0f);
				}
				else if (currentRpm >= maxRpm)
				{
					throttle = 0f;
				}
				else
				{
					throttle = controlInput;
				}*/
				
				throttle = throttleInput;

				float frictionTorque = minTorqueFriction + torqueFrictionForce * currentRpm;
				effectiveTorque = torqueCurve.Evaluate(currentRpm);
				float initialTorque = (effectiveTorque + frictionTorque) * throttle;

				effectiveTorque = initialTorque - frictionTorque - loadTorque;
				
				float angularAcceleration = effectiveTorque / inertia;
				currentAngularVelocity += angularAcceleration * Time.fixedDeltaTime;
				
				currentAngularVelocity = Mathf.Clamp(currentAngularVelocity, 
					minRpm * MathE.Rpm2Rad, maxRpm * MathE.Rpm2Rad);
				//currentAngularVelocity = Mathf.Min(currentAngularVelocity, maxRpm * MathE.Rpm2Rad);

				currentRpm = currentAngularVelocity * MathE.Rad2Rpm;
			}
		}
	}
}
