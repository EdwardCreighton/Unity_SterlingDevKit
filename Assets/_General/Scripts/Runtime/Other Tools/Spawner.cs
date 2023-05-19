using UnityEngine;

namespace SterlingTools
{
	public class Spawner : MonoBehaviour
	{
		#region Fields

		[SerializeField] private GameObject clonePrefab;
		[Space]
		[SerializeField] private Vector3Int cloneCount = new Vector3Int(1, 1, 1);
		[SerializeField] private Vector3 spacing;
		[SerializeField] private Vector3 offset;

		#endregion

		[ContextMenu("Update Clones")]
		private void UpdateClones()
		{
			Delete();
			Spawn();
		}

		private void Spawn()
		{
			if (!clonePrefab) return;
			
			cloneCount.x = Mathf.Max(cloneCount.x, 0);
			cloneCount.y = Mathf.Max(cloneCount.y, 0);
			cloneCount.z = Mathf.Max(cloneCount.z, 0);
			
			for (int rows = 0; rows < cloneCount.x; rows++)
			{
				for (int columns = 0; columns < cloneCount.z; columns++)
				{
					for (int height = 0; height < cloneCount.y; height++)
					{
						GameObject clone = Instantiate(clonePrefab, transform);
						
						Vector3 pos = new Vector3(rows * spacing.x, height * spacing.y, columns * spacing.z);
						pos += offset;

						clone.transform.localPosition = pos;
					}
				}
			}
		}

		[ContextMenu("Delete Clones")]
		private void Delete()
		{
			int childCount = transform.childCount;

			for (int i = 0; i < childCount; i++)
			{
				if (Application.isPlaying) Destroy(transform.GetChild(0).gameObject);
				else DestroyImmediate(transform.GetChild(0).gameObject);
			}
		}
	}
}
