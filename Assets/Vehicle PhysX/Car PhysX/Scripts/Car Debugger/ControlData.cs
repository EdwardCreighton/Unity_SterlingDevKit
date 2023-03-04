using System;
using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	[Serializable]
	public class ControlData
	{
		[Header("Overall")]
		public float carSpeed;
		
		[Header("Engine")]
		public float engineThrottle;
		public float engineRpm;
		public float maxRpm;
		public float engineAngVel;
		public float engineTorque;

		[Header("Clutch")]
		public float clutchAngVel;
		public float clutchTorque;

		[Header("Gearbox")]
		public int currentGearIndex;

		[Header("Shaft")]
		public float shaftTorque;
		public float shaftVelocity;

		[Header("Wheels")]
		public List<AxleInfo> axleInfos;

		[Serializable]
		public class AxleInfo
		{
			public float leftWheelsSlip;
			public float leftWheelsForce;
			public float leftWheelsAngVel;
			
			public float rightWheelsSlip;
			public float rightWheelsForce;
			public float rightWheelsAngVel;
		}
	}
}
