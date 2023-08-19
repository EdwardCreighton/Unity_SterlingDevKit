using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SterlingTools
{
	[DefaultExecutionOrder(100)]
	public class TiledSceneTeleport : MonoBehaviour
	{
		[SerializeField] private Vector3 bounds;
		[SerializeField] private List<Transform> targets;

		public List<Transform> Targets
		{
			get => targets;
			set => targets = value;
		}

		private void FixedUpdate()
		{
			foreach (Transform target in Targets.Where((x) => x))
			{
				bool tp = false;
				
				Vector3 relPos = target.position - transform.position;
				Vector3 newPos = relPos;
				
				if (Mathf.Abs(relPos.x) > bounds.x / 2f)
				{
					tp = true;
					newPos.x -= bounds.x * Mathf.Sign(relPos.x);
				}

				if (Mathf.Abs(relPos.y) > bounds.y / 2f)
				{
					tp = true;
					newPos.y -= bounds.y * Mathf.Sign(relPos.y);
				}

				if (Math.Abs(relPos.z) > bounds.z / 2f)
				{
					tp = true;
					newPos.z -= bounds.z * Mathf.Sign(relPos.z);
				}

				if (tp) target.position = newPos + transform.position;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(bounds.x, bounds.y, bounds.z));
		}
	}
}
