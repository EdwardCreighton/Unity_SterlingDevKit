using System;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[Serializable]
	public class FrictionData
	{
		public Vector2 tireStiffness;

		[HideInInspector] public Vector2 tireForce;
	}
}
