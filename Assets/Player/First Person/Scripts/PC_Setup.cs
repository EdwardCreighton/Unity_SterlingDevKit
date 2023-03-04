using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.Player.POV
{
	public partial class PlayerComponent
	{
		private void SetRigidbodyRef()
		{
			if (rigidbody) return;

			TryGetComponent(out rigidbody);

			if (!rigidbody)
			{
				rigidbody = gameObject.AddComponent<Rigidbody>();
			}
		}

		private void SetRigidbody()
		{
			if (!rigidbody)
			{
				PrintNullRefError("player's rigidbody", this);
				return;
			}

			rigidbodySettings.SetRigidbody(rigidbody);
		}

		private void SetColliderRef()
		{
			if (collider) return;

			Transform colliderHolder = transform.Find(PlayerConfig.BodyCollider);
			
			collider = colliderHolder.GetComponentInChildren<CapsuleCollider>();

			if (collider) return;

			GameObject colliderGo = new GameObject("Collider")
			{
				transform =
				{
					parent = colliderHolder,
					localPosition = Vector3.zero,
					localRotation = Quaternion.identity
				}
			};

			collider = colliderGo.AddComponent<CapsuleCollider>();
		}

		private void SetCollider()
		{
			if (!collider)
			{
				PrintNullRefError("player's collider", this);
				return;
			}
			
			colliderSettings.SetCollider(collider);
		}

		private void SetPlayerData()
		{
			playerData = new PlayerData
			{
				playerComponent = this,
				rigidbody = rigidbody,
				collisionPoints = new List<ContactPoint>(0),
			};
		}

		private void SetModulesRefs()
		{
			if (TryGetComponent(out movementModule)) movementModule.playerData = playerData;
		}

		private void PrintNullRefError(string missingRef, Object context)
		{
			Debug.LogError($"Missing reference to {missingRef}", context);
		}
	}
}
