using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[CreateAssetMenu(fileName = "High Clutch Friction", menuName = "Sterling/Vehicle PhysX/Wheel/Friction/High Clutch")]
	public class HighClutchFriction : FrictionProfile
	{
		public override void Sequence(WheelData wheelData)
		{
			wheelData.frictionData.tireForce.y = wheelData.Slip;
			wheelData.frictionData.tireForce.x = -wheelData.linearVelocity.x;

			wheelData.frictionData.tireForce *= Mathf.Max(wheelData.suspensionData.upForce, 0f);
			
			Vector3 suspensionForce = Mathf.Max(wheelData.suspensionData.upForce, 0f) * wheelData.wheelComponentTransform.up;
			Vector3 suspensionGroundProject = new Vector3(suspensionForce.x, 0f, suspensionForce.z);
			suspensionGroundProject = wheelData.wheelComponentTransform.InverseTransformDirection(suspensionGroundProject);

			wheelData.frictionData.tireForce.x -= suspensionGroundProject.x;
			wheelData.frictionData.tireForce.y -= suspensionGroundProject.z;

			wheelData.frictionData.tireForce = Vector2.ClampMagnitude(wheelData.frictionData.tireForce, Mathf.Max(wheelData.suspensionData.upForce, 0f));
		}
	}
}
