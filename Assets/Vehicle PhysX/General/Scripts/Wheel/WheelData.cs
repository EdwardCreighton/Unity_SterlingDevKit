using System;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[Serializable]
	public class WheelData
	{
		public SuspensionProfile suspensionProfile;
		public FrictionProfile frictionProfile;
		[Space]
		public SuspensionData suspensionData;
		public FrictionData frictionData;
		[Space]
		public float mass = 15f;

		[HideInInspector] public RaycastHit hitInfo;
		[HideInInspector] public Vector3 linearVelocity;
		[HideInInspector] public float angularVelocity;

		[HideInInspector] public float driveTorque;
		[HideInInspector] public float brakeTorque;
		
		[HideInInspector] public float radius;
		[HideInInspector] public float inertia;

		[HideInInspector] public float penetrationDistance;

		[HideInInspector] public Transform wheelComponentTransform;

		[HideInInspector] public float collisionImpulseResolve;
		[HideInInspector] public float collisionPositionResolve;

		public float Slip => angularVelocity * radius - linearVelocity.z;
	}
}
