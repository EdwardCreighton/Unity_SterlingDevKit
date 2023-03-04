using System;
using UnityEngine;

namespace SterlingTools
{
	public class MeshGenerator : MonoBehaviour
	{
		#region Fields

		[SerializeField] private MeshFilter meshFilter;
		[SerializeField] private MeshCollider meshCollider;
		[Space]
		[SerializeField] private MeshType meshType;

		[Space(order = 0)] [Header("Plane", order = 1)]
		[SerializeField] private Vector2 planeSize;

		[Space(order = 0)] [Header("Cube", order = 1)]
		[SerializeField] private Vector3 cubeSize;

		#endregion

		[ContextMenu("Spawn Mesh")]
		private void SpawnMesh()
		{
			SetMeshFilter();
			
			switch (meshType)
			{
				case MeshType.Plane:
				{
					PlaneGenerator generator = new PlaneGenerator();

					generator.size = planeSize;
					generator.CreateMesh();

					meshFilter.mesh = generator.Mesh;
					meshCollider.sharedMesh = generator.Mesh;

					break;
				}
				case MeshType.Cube:
				{
					CubeGenerator generator = new CubeGenerator();

					generator.size = cubeSize;
					generator.CreateMesh();

					meshFilter.mesh = generator.Mesh;
					meshCollider.sharedMesh = generator.Mesh;
					
					break;
				}
				case MeshType.Cylinder:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetMeshFilter()
		{
			if (meshFilter) return;

			gameObject.TryGetComponent(out meshFilter);

			if (meshFilter) return;

			meshFilter = gameObject.AddComponent<MeshFilter>();
		}
		
		private enum MeshType
		{
			Plane,
			Cube,
			Cylinder
		}
	}
}
