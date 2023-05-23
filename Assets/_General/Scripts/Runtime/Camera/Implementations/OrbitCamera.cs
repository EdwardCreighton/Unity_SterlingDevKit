using UnityEngine;

namespace SterlingAssets
{
	[CreateAssetMenu(fileName = "Orbit Camera", menuName = "Sterling/Camera/Orbit")]
	public class OrbitCamera : CameraProfile
	{
		#region Fields

		[SerializeField] private float distance = 2f;
		[Space]
		[SerializeField] private float pitchHigh = 60f;
		[SerializeField] private float pitchLow = -10f;
		[Space]
		[SerializeField] private float pitchSensitivity = 1f;
		[SerializeField] private float yawSensitivity = 1f;

		private Vector2 newAngles;

		#endregion

		public override void Sequence(CameraData data)
		{
			Transform cameraTransform = data.camera.transform;
			
			ComputeRotationAngles(data);

			ComputeDirectionVector(data);

			cameraTransform.position = data.followTarget + data.followOffset * distance;

			data.lookAtTarget = data.followTarget;
			cameraTransform.LookAt(data.lookAtTarget);
		}

		private void ComputeRotationAngles(CameraData data)
		{
			newAngles = data.followOffsetEulerAngles;
			newAngles.x += data.inputDelta.y * Time.deltaTime * pitchSensitivity;
			newAngles.y += data.inputDelta.x * Time.deltaTime * yawSensitivity;

			newAngles.x = Mathf.Clamp(newAngles.x, pitchLow, pitchHigh);

			if (newAngles.y > 360f)
			{
				newAngles.y -= 360f;
			}
			else if (newAngles.y < 0f)
			{
				newAngles.y += 360f;
			}

			data.followOffsetEulerAngles = newAngles;
		}

		private void ComputeDirectionVector(CameraData data)
		{
			data.followOffset = Quaternion.Euler(data.followOffsetEulerAngles.x, data.followOffsetEulerAngles.y, 0f) * Vector3.forward;
			data.followOffset.Normalize();
		}
	}
}
