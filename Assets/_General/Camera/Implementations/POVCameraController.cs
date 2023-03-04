using UnityEngine;

namespace SterlingAssets
{
	public class POVCameraController : CameraControllerBase
	{
		[SerializeField] private float yawSensitivity = 5f;
		[SerializeField] private float pitchSensitivity = 5f;
		[Space]
		[SerializeField] private float maxPitch = 90f;
		[SerializeField] private float minPitch = -90f;
		[Space]
		[SerializeField] private bool yAxisInvert = true;

		public override void OnAwake()
		{
			cameraData = SterlingCamera.Instance.CameraData;
		}

		public override void OnUpdate()
		{
			
		}

		public override void OnLateUpdate()
		{
			if (IsBusy)
			{
				TaskSequence(cameraData);
			}
			else
			{
				CameraSequence();
			}
		}

		public override void OnEditorSetup()
		{
			cameraData = SterlingCamera.Instance.CameraData;
			
			Transform cameraTransform = cameraData.camera.transform;
			cameraData.followTarget = cameraData.followPoint.position;
			cameraTransform.position = cameraData.followTarget;

			cameraData.lookAtTargetEulerAngles = new Vector2(cameraTransform.localEulerAngles.x, cameraTransform.localEulerAngles.y);

			if (cameraData.lookAtTargetEulerAngles.x is >= 270f and <= 360f)
			{
				cameraData.lookAtTargetEulerAngles.x -= 360f;
			}

			cameraData.lookAtTargetEulerAngles.x = Mathf.Clamp(cameraData.lookAtTargetEulerAngles.x, minPitch, maxPitch);
			
			Quaternion rotation = Quaternion.Euler(cameraData.lookAtTargetEulerAngles.x, cameraData.lookAtTargetEulerAngles.y, 0f);
			//cameraTransform.localRotation = rotation;
			
			//cameraData.lookAtTarget = cameraTransform.position + cameraTransform.forward;
		}

		private void CameraSequence()
		{
			Transform cameraTransform = cameraData.camera.transform;
			
			Place();
			Rotate();

			cameraData.lookAtTarget = cameraTransform.position + cameraTransform.forward;
		}
		
		private void Rotate()
		{
			Transform cameraTransform = cameraData.camera.transform;
			
			cameraData.lookAtTarget = cameraTransform.forward;

			if (cameraData.inputDelta.sqrMagnitude <= 0.1f) return;

			cameraData.lookAtTargetEulerAngles.y += cameraData.inputDelta.x * yawSensitivity * Time.deltaTime;
			cameraData.lookAtTargetEulerAngles.x += cameraData.inputDelta.y * pitchSensitivity * Time.deltaTime * (yAxisInvert ? -1f : 1f);
			
			if (cameraData.lookAtTargetEulerAngles.y > 360f)
			{
				cameraData.lookAtTargetEulerAngles.y -= 360f;
			}
			else if (cameraData.lookAtTargetEulerAngles.y < 0f)
			{
				cameraData.lookAtTargetEulerAngles.y += 360f;
			}
			
			cameraData.lookAtTargetEulerAngles.x = Mathf.Clamp(cameraData.lookAtTargetEulerAngles.x, minPitch, maxPitch);

			Quaternion rotation = Quaternion.Euler(cameraData.lookAtTargetEulerAngles.x, cameraData.lookAtTargetEulerAngles.y, 0f);

			cameraTransform.localRotation = rotation;
		}

		private void Place()
		{
			Transform cameraTransform = cameraData.camera.transform;
			cameraData.followTarget = cameraData.followPoint.position;

			cameraTransform.position = cameraData.followTarget;
		}
	}
}
