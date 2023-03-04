using System;
using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel>
	{
		[Serializable]
		public class Gearbox
		{
			[SerializeField] private List<float> gearsRatios = new List<float>() {0f, 2.7f, 2.3f, 2.1f, 1.6f, 1.3f, 0.9f};
			[SerializeField] private float reverseRatio = -2.7f;
			[SerializeField] private float shiftingTime = 0.3f;

			private int currentGearIndex;
			private int nextGearIndex;

			private float inputVelocity;
			private float outputVelocity;

			private bool isShifting;
			private float shiftingTimer;

			private float shaftVelocity;

			public float InputVelocity => inputVelocity;
			public float OutputVelocity => outputVelocity;
			public int CurrentGearIndex => currentGearIndex;

			public void UpdateGears(float input)
			{
				if (isShifting)
				{
					if (shiftingTimer >= shiftingTime)
					{
						isShifting = false;
						currentGearIndex = nextGearIndex;
						shiftingTimer = 0f;
					}

					shiftingTimer += Time.deltaTime;
					
					return;
				}

				if (input > 0)
				{
					if (currentGearIndex >= gearsRatios.Count - 1) return;

					nextGearIndex = currentGearIndex + 1;
					currentGearIndex = 0;
					isShifting = true;
				}
				else if (input < 0)
				{
					if (currentGearIndex <= -1) return;

					nextGearIndex = currentGearIndex - 1;
					currentGearIndex = 0;
					isShifting = true;
				}
			}

			public float GetCurrentGearRatio()
			{
				if (currentGearIndex <= -1)
				{
					return reverseRatio;
				}

				if (currentGearIndex >= gearsRatios.Count)
				{
					return gearsRatios[^1];
				}

				return gearsRatios[currentGearIndex];
			}
		}
	}
}
