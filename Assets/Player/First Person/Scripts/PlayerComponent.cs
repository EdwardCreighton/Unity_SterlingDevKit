using System;
using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.Player.POV
{
	[SelectionBase]
	public partial class PlayerComponent : MonoBehaviour
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

		private void Awake()
		{
			SetRigidbodyRef();
			SetRigidbody();
			SetColliderRef();
			SetCollider();
			SetPlayerData();
			SetModulesRefs();
		}

		private void Update()
		{
			if (movementModule) movementModule.OnUpdate();
		}

		private void FixedUpdate()
		{
			if (movementModule) movementModule.OnFixedUpdate();
		}

		private void LateUpdate()
		{
			if (movementModule) movementModule.OnLateUpdate();
		}

		private void OnCollisionEnter(Collision collision)
		{
			List<ContactPoint> contactPoints = new List<ContactPoint>();
			collision.GetContacts(contactPoints);

			playerData.collisionPoints = contactPoints;
		}

		private void OnCollisionStay(Collision collision)
		{
			List<ContactPoint> contactPoints = new List<ContactPoint>();
			collision.GetContacts(contactPoints);
			
			playerData.collisionPoints = contactPoints;
		}

		private void OnCollisionExit(Collision collision)
		{
			/*List<ContactPoint> contactPoints = new List<ContactPoint>();
			collision.GetContacts(contactPoints);
			
			UpdateCollisionPoints(contactPoints, true);*/
		}

		private void UpdateCollisionPoints(List<ContactPoint> contactPoints, bool remove)
		{
			/*if (remove)
			{
				foreach (ContactPoint contactPoint in contactPoints)
				{
					if (playerData.collisionPoints.Contains(contactPoint))
					{
						playerData.collisionPoints.Remove(contactPoint);
					}
				}

				return;
			}

			foreach (ContactPoint contactPoint in contactPoints)
			{
				

				playerData.collisionPoints.Add(contactPoint);
			}*/
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying) return;

			Gizmos.color = Color.red;
			
			foreach (ContactPoint contact in playerData.collisionPoints)
			{
				Gizmos.DrawSphere(contact.point, 0.1f);
			}
		}
	}
}
