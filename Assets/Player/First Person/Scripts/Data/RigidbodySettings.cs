using System;
using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[Serializable]
	public class RigidbodySettings
	{
		public float mass = 80f;
		public float drag = 0f;
		public float angularDrag = 0.05f;
		public RigidbodyInterpolation interpolation = RigidbodyInterpolation.Interpolate;
		public CollisionDetectionMode collisionDetection = CollisionDetectionMode.Discrete;
		public RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotation;

		public void SetRigidbody(Rigidbody rigidbody)
		{
			rigidbody.mass = mass;
			rigidbody.drag = drag;
			rigidbody.angularDrag = angularDrag;
			rigidbody.interpolation = interpolation;
			rigidbody.collisionDetectionMode = collisionDetection;
			rigidbody.constraints = constraints;
		}
	}
}
