using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	[CreateAssetMenu(fileName = "Collider Suspension", menuName = "Sterling/Vehicle PhysX/Wheel/Suspension/With collision check")]
	public class ColliderSuspension : SuspensionProfile
	{
		public override void Sequence(WheelData data)
		{
			SuspensionData suspensionData = data.suspensionData;

			suspensionData.lastLength = suspensionData.currentLength;
			
			if (data.hitInfo.transform)
			{
				data.penetrationDistance = (data.hitInfo.distance - data.suspensionData.MinLength) * -1f;
				data.penetrationDistance = Mathf.Max(data.penetrationDistance, 0f);
				
				suspensionData.currentLength = data.hitInfo.distance;
				suspensionData.currentLength = Mathf.Max(suspensionData.currentLength, suspensionData.MinLength);
				
				float delta = suspensionData.restLength - suspensionData.currentLength;
				float springForce = delta * suspensionData.springStiffness;
				float springVelocity = (suspensionData.lastLength - suspensionData.currentLength) / Time.fixedDeltaTime;
				float damperForce = springVelocity * suspensionData.damperStiffness;

				suspensionData.upForce = springForce + damperForce;
				suspensionData.upForce = Mathf.Max(suspensionData.upForce, 0f);
			}
			else
			{
				suspensionData.upForce = 0f;
				suspensionData.currentLength = suspensionData.MaxLength;
			}
		}
	}
}
