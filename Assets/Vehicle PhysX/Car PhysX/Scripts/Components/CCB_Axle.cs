using System;
using System.Collections.Generic;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		private class Axle
		{
			#region Fields

			[SerializeField] private float differentialRatio = 3.2f;
			[SerializeField] private float brakeRatio = 0.6f;
			
			[SerializeField] private bool driveEnabled;
			[SerializeField] private bool steeringEnabled;

			[SerializeField] private AnimationCurve brakeFactorCurve = new AnimationCurve(
				new Keyframe[]
				{
					new Keyframe(0f, 1f, -3.0661046504974367f, -3.0661046504974367f),
					new Keyframe(1f, 0f)
				});
			[SerializeField] private AnimationCurve driveFactorCurve = new AnimationCurve(
				new Keyframe[]
				{
					new Keyframe(0f, 1f),
					new Keyframe(1f, 1f)
				});
			
			[SerializeField] private float zLevel;
			[SerializeField] private List<TWheel> wheelComponents;

			#endregion

			#region Getters

			public bool DriveEnabled => driveEnabled;

			#endregion

			public Axle(TWheel wheelComponent)
			{
				zLevel = wheelComponent.transform.localPosition.z;
				driveEnabled = true;
				steeringEnabled = zLevel >= 0f;
				
				wheelComponents = new List<TWheel> { wheelComponent };
			}

			public bool AddWheelComponent(TWheel wheelComponent)
			{
				if (!Mathf.Approximately(wheelComponent.transform.localPosition.z, zLevel)) return false;
				
				wheelComponents.Add(wheelComponent);
				return true;
			}

			public void SetSteerAngle(float steerAngle)
			{
				if (!steeringEnabled) return;

				foreach (TWheel wheelComponent in wheelComponents)
				{
					wheelComponent.transform.localRotation = Quaternion.Euler(Vector3.up * steerAngle);
				}
			}

			public void UpdatePhysics(float shaftTorque, float brakeTorque)
			{
				float oneWheelTorque = shaftTorque * differentialRatio / wheelComponents.Count;

				float averageSlip = 0f;

				foreach (TWheel wheelComponent in wheelComponents)
				{
					averageSlip += Mathf.Abs(wheelComponent.WheelData.Slip);
				}

				averageSlip /= wheelComponents.Count;
				
				float brakeFactor = brakeFactorCurve.Evaluate(averageSlip);
				float driveFactor = driveFactorCurve.Evaluate(averageSlip);
				
				foreach (TWheel wheelComponent in wheelComponents)
				{
					if (driveEnabled) wheelComponent.WheelData.driveTorque = oneWheelTorque * driveFactor;

					wheelComponent.WheelData.brakeTorque = brakeTorque * brakeRatio * brakeFactor;
					
					wheelComponent.UpdatePhysics();
				}
			}

			public float GetAngularVelocity()
			{
				float shaftVelocity = 0f;

				foreach (TWheel wheel in wheelComponents)
				{
					if (driveEnabled) shaftVelocity += wheel.WheelData.angularVelocity;
				}

				return shaftVelocity * differentialRatio / wheelComponents.Count;
			}

			public void GetSlip(out float leftWheelsSlip, out float rightWheelsSlip)
			{
				leftWheelsSlip = 0f;
				rightWheelsSlip = 0f;

				foreach (TWheel component in wheelComponents)
				{
					float tempSlip = component.WheelData.Slip;

					if (component.transform.localPosition.x > 0f)
					{
						rightWheelsSlip += tempSlip;
					}
					else
					{
						leftWheelsSlip += tempSlip;
					}
				}
			}

			public void GetForce(out float leftWheelsForce, out float rightWheelsForce)
			{
				leftWheelsForce = 0f;
				rightWheelsForce = 0f;

				foreach (TWheel component in wheelComponents)
				{
					float tempForce = component.WheelData.frictionData.tireForce.y;

					if (component.transform.localPosition.x > 0f)
					{
						rightWheelsForce += tempForce;
					}
					else
					{
						leftWheelsForce += tempForce;
					}
				}
			}

			public void GetAngVel(out float leftWheelsAngVel, out float rightWheelsAngVel)
			{
				leftWheelsAngVel = 0f;
				rightWheelsAngVel = 0f;
				
				foreach (TWheel wheel in wheelComponents)
				{
					float tempVel = wheel.WheelData.angularVelocity;

					if (wheel.transform.localPosition.x > 0)
					{
						rightWheelsAngVel += tempVel;
					}
					else
					{
						leftWheelsAngVel += tempVel;
					}
				}
			}
		}
	}
}
