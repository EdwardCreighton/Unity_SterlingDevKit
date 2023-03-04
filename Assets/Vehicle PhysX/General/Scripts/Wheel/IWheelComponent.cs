using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	public interface IWheelComponent
	{
		public UpdateMode UpdateMode { get; set; }
		public Transform WheelTransform { get; set; }
		public Transform CaliperTransform { get; set; }
		public Rigidbody VehicleRigidbody { get; set; }
		public List<Collider> VehicleBodyColliders { get; set; }
		public WheelData WheelData { get; set; }

		public void CreateCollider();

		public void UpdatePhysics();
	}
}
