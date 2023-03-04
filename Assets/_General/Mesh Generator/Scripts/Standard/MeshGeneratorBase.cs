using UnityEngine;

namespace SterlingTools
{
	public abstract class MeshGeneratorBase
	{
		#region Fields

		protected Mesh mesh;

		protected Vector3[] vertices;
		protected Vector2[] uv;
		protected int[] triangles;

		#endregion

		#region Getters

		public Mesh Mesh => mesh;

		#endregion

		public abstract void CreateMesh();
	}
}
