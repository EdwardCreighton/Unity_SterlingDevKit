using System;
using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		protected class Drivetrain
		{
			#region Fields

			[SerializeField] private List<Axle> axles;
			[SerializeField] private Engine engine;
			[SerializeField] private Clutch clutch;
			[SerializeField] private Gearbox gearbox;
			[Space]
			[SerializeField] private float brakeTorque = 4000f;

			private float shaftTorque;
			private float shaftVelocity;

			#endregion

			public void SetAxles(List<TWheel> wheelComponents)
			{
				axles = new List<Axle>();

				foreach (var wheelComponent in wheelComponents)
				{
					bool placeExists = false;
					
					foreach (Axle axle in axles)
					{
						placeExists = axle.AddWheelComponent(wheelComponent);

						if (placeExists) break;
					}

					if (placeExists) continue;
					
					Axle newAxle = new Axle(wheelComponent);
					axles.Add(newAxle);
				}
			}

			public void Update(float steerAngle, float gearShiftInput)
			{
				foreach (var axle in axles)
				{
					axle.SetSteerAngle(steerAngle);
				}
				
				gearbox.UpdateGears(gearShiftInput);
			}

			public void FixedUpdate(float throttleInput, float clutchInput, float brakeInput)
			{
				shaftTorque = gearbox.GetCurrentGearRatio() * clutch.EffectiveTorque;
				
				foreach (Axle axle in axles)
				{
					axle.UpdatePhysics(shaftTorque, brakeTorque * brakeInput);
				}

				shaftVelocity = 0f;
				int driveAxles = 0;

				foreach (Axle axle in axles)
				{
					if (axle.DriveEnabled)
					{
						driveAxles++;
						shaftVelocity += axle.GetAngularVelocity();
					}
				}

				if (driveAxles > 1) shaftVelocity /= driveAxles;

				float gearboxVelocity = shaftVelocity * gearbox.GetCurrentGearRatio();
				
				clutch.TorqueFlow(engine.CurrentAngularVelocity, gearboxVelocity, gearbox.GetCurrentGearRatio(), clutchInput);
				
				engine.TorqueFlow(throttleInput, clutch.EffectiveTorque);
			}

			public void GetControlData(ControlData controlData)
			{
				controlData.engineThrottle = engine.Throttle;
				controlData.engineRpm = engine.CurrentRpm;
				controlData.maxRpm = engine.MaxRpm;
				controlData.engineAngVel = engine.CurrentAngularVelocity;
				controlData.engineTorque = engine.EffectiveTorque;

				controlData.clutchAngVel = clutch.CurrentAngularVelocity;
				controlData.clutchTorque = clutch.EffectiveTorque;

				controlData.currentGearIndex = gearbox.CurrentGearIndex;

				controlData.shaftTorque = shaftTorque;
				controlData.shaftVelocity = shaftVelocity;

				if (controlData.axleInfos.Count != axles.Count)
				{
					controlData.axleInfos = new List<ControlData.AxleInfo>(axles.Count);
					for (int i = 0; i < axles.Count; i++)
					{
						controlData.axleInfos.Add(new ControlData.AxleInfo());
					}
				}

				for (int i = 0; i < axles.Count; i++)
				{
					axles[i].GetSlip(out float leftWheelsSlip, out float rightWheelsSlip);
					axles[i].GetForce(out float leftWheelsForce, out float rightWheelsForce);
					axles[i].GetAngVel(out float leftWheelsAngVel, out float rightWheelsAngVel);

					controlData.axleInfos[i].leftWheelsSlip = leftWheelsSlip;
					controlData.axleInfos[i].leftWheelsForce = leftWheelsForce;
					controlData.axleInfos[i].leftWheelsAngVel = leftWheelsAngVel;

					controlData.axleInfos[i].rightWheelsSlip = rightWheelsSlip;
					controlData.axleInfos[i].rightWheelsForce = rightWheelsForce;
					controlData.axleInfos[i].rightWheelsAngVel = rightWheelsAngVel;
				}
			}
		}
	}
}
