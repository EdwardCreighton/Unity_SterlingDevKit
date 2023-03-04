using System;
using UnityEngine;

namespace SterlingTools
{
	[Serializable]
	public class PD_Controller
	{
		[SerializeField] private float pCoefficient = 1;
		[SerializeField] private float dCoefficient;

		private float lastValue;

		public float GetAdjustedValue(float currentValue, float targetValue, float deltaTime)
		{
			float error = targetValue - currentValue;
			float p = error;
			float d = (lastValue - currentValue) / deltaTime;

			lastValue = currentValue;

			return p * pCoefficient + d * dCoefficient;
		}
	}

	[Serializable]
	public class PID_Controller
	{
		
	}
}
