using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[SelectionBase]
	public partial class PlayerComponent : GameLoopEntity
	{
		#region Fields

		[SerializeField] private RigidbodySettings rigidbodySettings;
		[SerializeField] private ColliderSettings colliderSettings;
		
		private Rigidbody rigidbody;
		private CapsuleCollider collider;
		
		private PlayerData playerData;

		#endregion
		
		#region Components

		private MovementModule movementModule;

		#endregion

		public override void OnAwake()
		{
			base.OnAwake();
			
			SetRigidbodyRef();
			SetRigidbody();
			SetColliderRef();
			SetCollider();
			SetPlayerData();
			SetModulesRefs();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			
			if (movementModule) movementModule.OnUpdate();
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			
			if (movementModule) movementModule.OnFixedUpdate();
		}

		public override void OnLateUpdate()
		{
			base.OnLateUpdate();
			
			if (movementModule) movementModule.OnLateUpdate();
		}
	}
}
