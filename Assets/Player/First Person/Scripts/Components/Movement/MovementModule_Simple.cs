using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[AddComponentMenu("Sterling/Player/Module Movement Simple")]
	public class MovementModule_Simple : MovementModule
	{
		[SerializeField] private float moveSpeed = 6f;

		public override void OnUpdate()
		{
			Vector3 move = GetCameraRelativeMoveDirection();

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
