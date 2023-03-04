using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	public partial class WheelComponent
	{
		private Vector2 currentVisualRotation;
		
		private void UpdateVisual()
		{
			Vector3 newPosition = transform.position - transform.up * wheelData.suspensionData.currentLength;
			
			wheelTransform.position = newPosition;
			if (caliperTransform) caliperTransform.position = newPosition;

			currentVisualRotation.x += wheelData.angularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime;
			
			if (currentVisualRotation.x > 360f)
			{
				currentVisualRotation.x -= 360f;
			}
			else if (currentVisualRotation.x < 0)
			{
				currentVisualRotation.x += 360f;
			}
			
			currentVisualRotation.y = transform.localRotation.eulerAngles.y;
			
			wheelTransform.localRotation = Quaternion.Euler(currentVisualRotation);
			if (caliperTransform) caliperTransform.localRotation = Quaternion.Euler(Vector3.up * currentVisualRotation.y);
		}
	}
}
