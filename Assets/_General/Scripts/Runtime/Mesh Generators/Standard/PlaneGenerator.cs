using UnityEngine;

namespace SterlingTools
{
	public class PlaneGenerator : MeshGeneratorBase
	{
		#region Fields

		public Vector2 size = new Vector2(1f, 1f);

		#endregion

		public override void CreateMesh()
		{
			mesh = new Mesh
			{
				name = "Procedural Plane"
			};

			size.x = Mathf.Max(size.x, 0f);
			size.y = Mathf.Max(size.y, 0f);

			SetPlaneData();

			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
		}
		
		private void SetPlaneData()
		{
			vertices = new Vector3[4];
			uv = new Vector2[4];
			triangles = new int[6];

			// The order doesn't matter, but must be good for triangles setup
			vertices[0] = new Vector3(-0.5f * size.x, -0.5f * size.y);
			vertices[1] = new Vector3(-0.5f * size.x, 0.5f * size.y);
			vertices[2] = new Vector3(0.5f * size.x, -0.5f * size.y);
			vertices[3] = new Vector3(0.5f * size.x, 0.5f * size.y);
			
			uv[0] = new Vector2(0f, 0f);
			uv[1] = new Vector2(0f, 1f);
			uv[2] = new Vector2(1f, 0f);
			uv[3] = new Vector2(1f, 1f);
			
			// Order DOES matter (Clockwise from forward direction)
			// Forward is a local Z-axis
			triangles[0] = 0;
			triangles[1] = 1;
			triangles[2] = 2;
			
			triangles[3] = 2;
			triangles[4] = 1;
			triangles[5] = 3;
		}
	}
}