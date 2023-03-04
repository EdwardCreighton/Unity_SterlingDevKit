using System;
using UnityEngine;

namespace SterlingAssets
{
	[Serializable]
	public class CameraData
	{
		public Camera camera;

		public Transform followPoint;

		public Vector3 followTarget;
		public Vector3 followOffset;
		public Vector3 lookAtTarget;

		public Vector3 inputDelta;

		public Vector2 followOffsetEulerAngles;
		public Vector2 lookAtTargetEulerAngles;
	}
}
