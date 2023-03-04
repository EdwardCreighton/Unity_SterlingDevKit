using UnityEngine;

namespace SterlingAssets.Player.POV
{
	public abstract class MovementModule : ComponentBase
	{
		protected Vector3 GetCameraRelativeMoveDirection()
		{
			Vector3 relativeInput = GetMoveInputV3();
			relativeInput = SterlingCamera.Instance.CameraData.camera.transform.TransformDirection(relativeInput);
			
			relativeInput.y = 0f;
			
			relativeInput.Normalize();

			return relativeInput;
		}

		protected Vector3 GetPlayerRelativeMoveDirection()
		{
			Vector3 relativeInput = GetMoveInputV3();
			relativeInput = playerData.rigidbody.transform.TransformDirection(relativeInput);

			relativeInput.y = 0f;
			
			relativeInput.Normalize();

			return relativeInput;
		}

		private Vector3 GetMoveInputV3()
		{
			Vector2 moveInput = SterlingInputManager.Instance.PlayerActions.Move.ReadValue<Vector2>();
			return new Vector3(moveInput.x, 0f, moveInput.y);
		}
	}
}
