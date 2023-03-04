using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[CreateAssetMenu(fileName = "Simple Friction", menuName = "Sterling/Vehicle PhysX/Wheel/Friction/Simple")]
	public class SimpleFriction : FrictionProfile
	{
		#region Fields

		[SerializeField] private float velocityStep = 5f;
		[SerializeField] private float velocityLimit = 25f;

		#endregion
		
		public override void Sequence(WheelData wheelData)
		{
			float torque = wheelData.Slip / Time.fixedDeltaTime / wheelData.radius * wheelData.inertia;
			wheelData.frictionData.tireForce.y = torque / wheelData.radius;
			wheelData.frictionData.tireForce.x = -wheelData.linearVelocity.x * Mathf.Max(wheelData.suspensionData.upForce, 0f);
			
			Vector3 suspensionForce = Mathf.Max(wheelData.suspensionData.upForce, 0f) * wheelData.wheelComponentTransform.up;
			Vector3 suspensionGroundProject = new Vector3(suspensionForce.x, 0f, suspensionForce.z);
			suspensionGroundProject = wheelData.wheelComponentTransform.InverseTransformDirection(suspensionGroundProject);

			wheelData.frictionData.tireForce.x -= suspensionGroundProject.x;
			wheelData.frictionData.tireForce.y -= suspensionGroundProject.z;

			wheelData.frictionData.tireForce = Vector2.ClampMagnitude(wheelData.frictionData.tireForce, Mathf.Max(wheelData.suspensionData.upForce, 0f));
		}
	}
}
