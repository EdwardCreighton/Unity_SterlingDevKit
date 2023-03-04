using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	public partial class WheelComponent : MonoBehaviour, IWheelComponent
	{
		#region Fields
		
		[SerializeField] private UpdateMode updateMode;
		[Space]
		[SerializeField] private Transform wheelTransform;
		[SerializeField] private Transform caliperTransform;
		[SerializeField] private Rigidbody vehicleRigidbody;
		[SerializeField] private List<Collider> vehicleBodyColliders;
		[Space]
		[SerializeField] private WheelData wheelData;

		private MeshFilter wheelMeshFilter;

		private Rigidbody wheelRigidbody;
		
		private MeshCollider wheelCollider;
		private Transform wheelColliderTransform;

		#endregion

		#region Properties

		public UpdateMode UpdateMode
		{
			get => updateMode;
			set => updateMode = value;
		}

		public Transform WheelTransform
		{
			get => wheelTransform;
			set => wheelTransform = value;
		}

		public Transform CaliperTransform
		{
			get => caliperTransform;
			set => caliperTransform = value;
		}

		public Rigidbody VehicleRigidbody
		{
			get => vehicleRigidbody;
			set => vehicleRigidbody = value;
		}

		public List<Collider> VehicleBodyColliders
		{
			get => vehicleBodyColliders;
			set => vehicleBodyColliders = value;
		}

		public WheelData WheelData
		{
			get => wheelData;
			set => wheelData = value;
		}

		#endregion

		private void Awake()
		{
			if (!wheelData.suspensionProfile)
				wheelData.suspensionProfile = ScriptableObject.CreateInstance<SuspensionProfile>();
			if (!wheelData.frictionProfile)
				wheelData.frictionProfile = ScriptableObject.CreateInstance<FrictionProfile>();

			CheckRefs();
			UpdateWheelData();
		}

		private void FixedUpdate()
		{
			if (updateMode == UpdateMode.Auto) UpdatePhysics();
			
			UpdateVisual();
		}
	}
}
