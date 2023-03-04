using SterlingTools;
using SterlingTools.VehiclePhysX.Car;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	public partial class WheelComponent
	{
		public void CreateCollider()
		{
			if (!CheckRefs()) return;

			UpdateCollider();
			
			SetRigidbody();

			wheelData.suspensionData = new SuspensionData();
			transform.localPosition += Vector3.up * (wheelData.suspensionData.restLength * 0.85f);
		}

		private bool CheckRefs()
		{
			if (!wheelTransform)
			{
				NullRefException("Wheel Transform");
				return false;
			}

			if (!wheelCollider)
			{
				if (!wheelColliderTransform)
				{
					wheelColliderTransform = transform.Find(CarPhysXConfig.WheelColliderTransformName);

					if (!wheelColliderTransform)
					{
						wheelColliderTransform = new GameObject(CarPhysXConfig.WheelColliderTransformName).transform;
						wheelColliderTransform.parent = transform;
					}
				}
				
				wheelColliderTransform.TryGetComponent(out wheelCollider);

				if (!wheelCollider) wheelCollider = wheelColliderTransform.gameObject.AddComponent<MeshCollider>();
			}

			if (!wheelRigidbody)
			{
				TryGetComponent(out wheelRigidbody);

				if (!wheelRigidbody) wheelRigidbody = gameObject.AddComponent<Rigidbody>();
			}

			if (!GetWheelMesh(wheelTransform))
			{
				NullRefException("Wheel Mesh");
				return false;
			}

			return true;
		}

		private void UpdateCollider()
		{
			Mesh sharedMesh = wheelMeshFilter.sharedMesh;
			Vector3 localScale = wheelTransform.localScale;

			CylinderGenerator generator = new CylinderGenerator
			{
				Height = sharedMesh.bounds.size.x * localScale.x,
				Width = sharedMesh.bounds.size.y * localScale.y,
				Vertices = 12,
			};
			
			generator.CreateMesh();

			wheelCollider.sharedMesh = generator.Mesh;

			foreach (Collider vehicleBodyCollider in vehicleBodyColliders)
			{
				Physics.IgnoreCollision(wheelCollider, vehicleBodyCollider);
			}

			wheelCollider.convex = true;
			wheelCollider.isTrigger = true;
			
			wheelColliderTransform.localPosition = Vector3.zero;
			wheelColliderTransform.localRotation = wheelTransform.rotation;
		}

		private void SetRigidbody()
		{
			wheelRigidbody.isKinematic = true;
			wheelRigidbody.useGravity = false;
		}

		private void UpdateWheelData()
		{
			wheelData.radius = wheelCollider.sharedMesh.bounds.size.y / 2f;
			wheelData.inertia = wheelData.mass * wheelData.radius * wheelData.radius / 2f;

			wheelData.wheelComponentTransform = transform;
		}

		private bool GetWheelMesh(Transform transformToParse)
		{
			if (wheelMeshFilter) return true;
			
			if (transformToParse.TryGetComponent(out wheelMeshFilter)) return true;

			int childCount = transformToParse.childCount;

			for (int i = 0; i < childCount; i++)
			{
				if (GetWheelMesh(transformToParse.GetChild(i)))
				{
					break;
				}
			}

			return (bool)wheelMeshFilter;
		}

		private void NullRefException(string missingObject)
		{
			Debug.LogError($"{missingObject} is missing in {name}, {transform.root.name}.");
		}
	}
}
