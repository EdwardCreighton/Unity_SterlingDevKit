using System;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[Serializable]
	public class SuspensionData
	{
		#region Fields

		public float springStiffness = 70000f;
		public float restLength = 0.4f;
		public float deltaLength = 0.1f;
		[Space]
		public float damperStiffness = 3000f;
		
		[HideInInspector] public float upForce;
		[HideInInspector] public float currentLength;
		[HideInInspector] public float lastLength;

		#endregion
		
		#region Properties

		public float MaxLength => restLength + deltaLength;
		public float MinLength => restLength - deltaLength;

		#endregion
	}
}
