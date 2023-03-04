using UnityEngine;

namespace SterlingTools
{
	public class CubeGenerator : MeshGeneratorBase
	{
		#region Fields

		public Vector3 size = new Vector3(1f, 1f, 1f);

		private Vector3[] meshCorners;

		private Vector2[] uvCorners = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f),
		};

		#endregion
		
		public override void CreateMesh()
		{
			mesh = new Mesh
			{
				name = "Procedural Cube"
			};

			size.x = Mathf.Max(size.x, 0);
			size.y = Mathf.Max(size.y, 0);
			size.z = Mathf.Max(size.z, 0);
			
			meshCorners = new Vector3[]
			{
				new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z), // Front Bottom Right
				new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z), // Front Bottom Left

				new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z), // Front Top Right
				new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z), // Front Top Left

				new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z), // Back Top Right
				new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z), // Back Top Left

				new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z), // Back Bottom Right
				new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z), // Back Bottom Left
			};

			SetCubeData();

			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			
			mesh.RecalculateNormals();
		}

		private void SetCubeData()
		{
			vertices = new Vector3[24];
			uv = new Vector2[24];
			triangles = new int[36];

			SetVertices();
			SetPolygons();
			SetSmartUVs();
		}

		private void SetVertices()
		{
			int cornerIndex = 0;
			
			// Front, Top, Back and Bottom
			for (int i = 0; i < 16; i++)
			{
				if (i != 0 && i % 4 == 0)
				{
					cornerIndex -= 2;
				}
			
				if (cornerIndex == 8)
				{
					cornerIndex = 0;
				}
				
				vertices[i] = meshCorners[cornerIndex];

				++cornerIndex;
			}

			// Left
			vertices[16] = meshCorners[1];
			vertices[17] = meshCorners[7];
			vertices[18] = meshCorners[3];
			vertices[19] = meshCorners[5];
			
			// Right
			vertices[20] = meshCorners[6];
			vertices[21] = meshCorners[0];
			vertices[22] = meshCorners[4];
			vertices[23] = meshCorners[2];
		}

		private void SetPolygons()
		{
			int side = 0;
			int polygonVertex = 0;
			int polygon = 0;
			
			for (int i = 0; i < 36; i++)
			{
				int vertexIndex = side * 4;

				switch (polygon)
				{
					case 0:
					{
						switch (polygonVertex)
						{
							case 0:
							{
								vertexIndex += 1;
								break;
							}
							case 2:
							{
								vertexIndex += 2;
								break;
							}
						}
						break;
					}
					case 1:
					{
						switch (polygonVertex)
						{
							case 0:
							{
								vertexIndex += 1;
								break;
							}
							case 1:
							{
								vertexIndex += 2;
								break;
							}
							case 2:
							{
								vertexIndex += 3;
								break;
							}
						}
						break;
					}
				}
				
				triangles[i] = vertexIndex;

				++polygonVertex;

				if (polygonVertex == 3)
				{
					++polygon;
					polygonVertex = 0;
				}

				if (polygon == 2)
				{
					++side;
					polygon = 0;
				}
			}
		}

		private void SetUVs()
		{
			int index = 0;
			
			for (int i = 0; i < 24; i++)
			{
				if (i != 0 && i % 4 == 0)
				{
					index = 0;
				}
				
				uv[i] = uvCorners[index];

				++index;
			}
		}

		private void SetSmartUVs()
		{
			SetUVs();
			
			// Back
			uv[8] = uvCorners[3];
			uv[9] = uvCorners[2];
			uv[10] = uvCorners[1];
			uv[11] = uvCorners[0];
		}
	}
}
