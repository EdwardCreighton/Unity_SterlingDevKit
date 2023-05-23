using UnityEngine;

namespace SterlingAssets
{
	[CreateAssetMenu(fileName = "Smooth Orbit Camera", menuName = "Sterling/Camera/Smooth Orbit")]
	public class SmoothOrbitCamera : CameraProfile
	{
		#region Fields

		[SerializeField] private float distance = 2f;
		[Space]
		[SerializeField] private float pitchHigh = 60f;
		[SerializeField] private float pitchLow = -10f;
		[Space]
		[SerializeField] private float pitchSensitivity = 1f;
		[SerializeField] private float yawSensitivity = 1f;
		[Space]
		[SerializeField] private float rotateSpeed = 2f;

		private Vector2 targetAngles = new Vector2(0f, 0f);

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
			targetAngles.x += data.inputDelta.y * Time.deltaTime * pitchSensitivity;
			targetAngles.y += data.inputDelta.x * Time.deltaTime * yawSensitivity;

			targetAngles.x = Mathf.Clamp(targetAngles.x, pitchLow, pitchHigh);

			if (targetAngles.y > 360f)
			{
				targetAngles.y -= 360f;
			}
			else if (targetAngles.y < 0)
			{
				targetAngles.y += 360f;
			}
		}

		private void ComputeDirectionVector(CameraData data)
		{
			Vector3 targetOffset = Quaternion.Euler(targetAngles.x, targetAngles.y, 0f) * Vector3.forward;
			data.followOffset = Vector3.Slerp(data.followOffset, targetOffset, rotateSpeed * Time.deltaTime);
			data.followOffset.Normalize();
		}
	}
}
