using System;
using System.Collections.Generic;
using UnityEngine;

namespace SterlingTools
{
	[DefaultExecutionOrder(100)]
	public class TiledSceneTeleport : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Vector2 bounds;
		[SerializeField] private List<Transform> objects;

		#endregion
		
		private void FixedUpdate()
		{
			foreach (Transform obj in objects)
			{
				bool tp = false;
				
				Vector3 relPos = obj.position - transform.position;
				Vector3 newPos = relPos;
				
				if (Mathf.Abs(relPos.x) > bounds.x / 2f)
				{
					tp = true;
					newPos.x -= bounds.x * Mathf.Sign(relPos.x);
				}

				if (Math.Abs(relPos.z) > bounds.y / 2f)
				{
					tp = true;
					newPos.z -= bounds.y * Mathf.Sign(relPos.z);
				}

				if (tp) obj.position = newPos + transform.position;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(bounds.x, 5f, bounds.y));
		}
	}
}
