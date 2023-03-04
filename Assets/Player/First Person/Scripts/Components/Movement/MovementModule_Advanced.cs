using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[AddComponentMenu("Sterling/Player/Module Movement Advanced")]
	public class MovementModule_Advanced : MovementModule
	{
		[SerializeField] private float moveSpeed = 6f;
	
		public override void OnUpdate()
		{
			Vector3 eulerAngles = playerData.rigidbody.transform.localRotation.eulerAngles;
			eulerAngles.y = SterlingCamera.Instance.CameraData.camera.transform.localRotation.eulerAngles.y;
			playerData.rigidbody.transform.localRotation = Quaternion.Euler(eulerAngles);
			
			Vector3 move = GetPlayerRelativeMoveDirection();
			playerData.rigidbody.velocity = move * moveSpeed;
		}

		public override void OnFixedUpdate()
		{
			
		}

		public override void OnLateUpdate()
		{
			
		}
	}
}
