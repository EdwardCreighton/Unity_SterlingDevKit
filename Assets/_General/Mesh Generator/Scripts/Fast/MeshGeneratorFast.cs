using UnityEngine;

namespace General
{
	public abstract class MeshGeneratorFast
	{
		#region Fields

		protected Mesh mesh;
		protected Mesh.MeshDataArray meshDataArray;
		
		#endregion

		#region Getters

		public Mesh Mesh => mesh;

		#endregion

		public abstract void CreateMesh();
		
		protected void AllocateMeshData(string meshName)
		{
			meshDataArray = Mesh.AllocateWritableMeshData(1);

			mesh = new Mesh()
			{
				name = meshName,
			};
		}

		protected void ApplyMeshData()
		{
			Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
		}
	}
}
