using System;
using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[Serializable]
	public class ColliderSettings
	{
		public float height = 2f;
		public float radius = 0.4f;

		public void SetCollider(CapsuleCollider collider)
		{
			collider.height = height;
			collider.radius = radius;
			collider.center = Vector3.up * height / 2f;

			PhysicMaterial material = new PhysicMaterial()
			{
				bounciness = 0f,
				bounceCombine = PhysicMaterialCombine.Multiply,
				dynamicFriction = 0f,
				staticFriction = 0f,
				frictionCombine = PhysicMaterialCombine.Multiply
			};

			collider.material = material;
		}
	}
}
