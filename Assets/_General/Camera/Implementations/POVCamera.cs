using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	[CreateAssetMenu(fileName = "POV Camera", menuName = "Sterling/Camera/POV")]
	public class POVCamera : CameraProfile
	{
		[SerializeField] private float yawSensitivity = 10f;
		[SerializeField] private float pitchSensitivity = 5f;
		[SerializeField] private bool yAxisInvert = true;
		
		public override void Sequence(CameraData data)
		{
			Transform cameraTransform = data.camera.transform;
			
			Place(data);
			Rotate(data);

			data.lookAtTarget = cameraTransform.position + cameraTransform.forward;
		}

		private void Rotate(CameraData data)
		{
			Transform cameraTransform = data.camera.transform;
			
			data.lookAtTarget = cameraTransform.forward;

			if (data.inputDelta.sqrMagnitude <= 0.1f) return;

			data.lookAtTargetEulerAngles.y += data.inputDelta.x * yawSensitivity * Time.deltaTime;
			data.lookAtTargetEulerAngles.x += data.inputDelta.y * pitchSensitivity * Time.deltaTime * (yAxisInvert ? -1f : 1f);
			
			if (data.lookAtTargetEulerAngles.y > 360f)
			{
				data.lookAtTargetEulerAngles.y -= 360f;
			}
			else if (data.lookAtTargetEulerAngles.y < 0f)
			{
				data.lookAtTargetEulerAngles.y += 360f;
			}

			data.lookAtTargetEulerAngles.x = Mathf.Clamp(data.lookAtTargetEulerAngles.x, -90f, 90f);

			Quaternion rotation = Quaternion.Euler(data.lookAtTargetEulerAngles.x, data.lookAtTargetEulerAngles.y, 0f);

			cameraTransform.localRotation = rotation;
		}

		private void Place(CameraData data)
		{
			Transform cameraTransform = data.camera.transform;
			data.followTarget = data.followPoint.position;

			cameraTransform.position = data.followTarget;
		}
	}
}
