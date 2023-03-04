using System.Collections.Generic;
using SterlingTools.VehiclePhysX.Car;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract partial class CarComponentBase<TWheel> : MonoBehaviour where TWheel : MonoBehaviour, IWheelComponent
	{
		#region Fields

		[SerializeField] protected SuspensionProfile suspensionProfile;
		[SerializeField] protected FrictionProfile frictionProfile;
		
		[SerializeField] protected Rigidbody rigidbody;
		[SerializeField] private List<Collider> colliders;
		
		[SerializeField] private List<TWheel> allWheelComponents;
		
		[SerializeField] protected Drivetrain drivetrain;
		[SerializeField] protected SteerWheel steerWheel;
		
		[SerializeField] private Transform bodyMeshHolder;
		[SerializeField] private Transform bodyColliderHolder;
		[SerializeField] private Transform wheelMeshHolder;
		[SerializeField] private Transform caliperMeshHolder;
		[SerializeField] private Transform wheelColliderHolder;
		
		#endregion
		
		protected void CreateCar()
		{
			SetTransformRefs();

			InitBodyColliders();
			InitRigidbody();

			CreateWheelColliders();

			InitDrivetrain();
		}
		
		protected void SetCenterOfMass()
		{
			Transform com = transform.Find(CarPhysXConfig.CenterOfMassName);
			
			if (com) rigidbody.centerOfMass = com.localPosition;
		}

		private void InitDrivetrain()
		{
			drivetrain.SetAxles(allWheelComponents);
		}

		private void InitBodyColliders()
		{
			colliders?.Clear();
			ParseBodyCollider(bodyColliderHolder);
		}

		private void InitRigidbody()
		{
			if (!rigidbody)
			{
				TryGetComponent(out rigidbody);

				if (!rigidbody) rigidbody = gameObject.AddComponent<Rigidbody>();
			}

			rigidbody.mass = 1800f;
			rigidbody.angularDrag = 0f;
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
		}

		private void SetTransformRefs()
		{
			bodyMeshHolder = transform.Find(CarPhysXConfig.BodyMeshHolderName);
			bodyColliderHolder = transform.Find(CarPhysXConfig.BodyColliderHolderName);
			wheelMeshHolder = transform.Find(CarPhysXConfig.WheelMeshHolderName);
			caliperMeshHolder = transform.Find(CarPhysXConfig.CaliperMeshHolderName);
			wheelColliderHolder = transform.Find(CarPhysXConfig.WheelColliderHolderName);
		}

		private void ParseBodyCollider(Transform transformToParse)
		{
			transformToParse.TryGetComponent(out Collider collider);

			if (collider) colliders.Add(collider);
			
			int childCount = transformToParse.childCount;

			for (int i = 0; i < childCount; i++)
			{
				ParseBodyCollider(transformToParse.GetChild(i));
			}
		}

		private void CreateWheelColliders()
		{
			while (wheelColliderHolder.childCount > 0)
			{
				if (Application.isEditor) DestroyImmediate(wheelColliderHolder.GetChild(0).gameObject);
				else Destroy(wheelColliderHolder.GetChild(0).gameObject);
			}
			
			int wheelsCount = wheelMeshHolder.childCount;
			int calipersCount = caliperMeshHolder.childCount;

			if (allWheelComponents == null)
			{
				allWheelComponents = new List<TWheel>();
			}
			else
			{
				allWheelComponents.Clear();
			}

			for (int i = 0; i < wheelsCount; i++)
			{
				GameObject wheelColliderGo = new GameObject(wheelMeshHolder.GetChild(i).name + " Collider");
				Transform wheelColliderTr = wheelColliderGo.transform;

				wheelColliderTr.position = wheelMeshHolder.GetChild(i).position;
				wheelColliderTr.parent = wheelColliderHolder;

				TWheel wheelComponent = wheelColliderGo.AddComponent<TWheel>();
				wheelComponent.WheelTransform = wheelMeshHolder.GetChild(i);
				
				if (i < calipersCount) wheelComponent.CaliperTransform = transform.Find(CarPhysXConfig.CaliperMeshHolderName).GetChild(i);
				wheelComponent.VehicleRigidbody = rigidbody;
				wheelComponent.VehicleBodyColliders = colliders;

				wheelComponent.WheelData = new WheelData
				{
					suspensionProfile = suspensionProfile,
					frictionProfile = frictionProfile
				};

				wheelComponent.UpdateMode = UpdateMode.Manual;
				
				wheelComponent.CreateCollider();

				allWheelComponents.Add(wheelComponent);
			}
		}
	}
}
