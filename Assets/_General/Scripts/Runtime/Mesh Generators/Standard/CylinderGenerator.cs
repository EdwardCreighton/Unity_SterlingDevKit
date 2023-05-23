using UnityEngine;

namespace SterlingTools
{
	public class CylinderGenerator : MeshGeneratorBase
	{
		#region Fields
		
		private float height = 1f;
		private float width = 1f;

		private int numberOfVertices = 4;

		#endregion

		#region Getter/Setter

		public int Vertices
		{
			get => numberOfVertices;
			set => numberOfVertices = Mathf.Max(value, 3);
		}

		public float Width
		{
			get => width;
			set => width = Mathf.Max(value, float.Epsilon);
		}
		
		public float Height
		{
			get => height;
			set => height = Mathf.Max(value, float.Epsilon);
		}

		#endregion

		public override void CreateMesh()
		{
			mesh = new Mesh
			{
				name = "Procedural Cylinder"
			};

			SetCylinderData();

			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;

			mesh.RecalculateNormals();
		}

		private void SetCylinderData()
		{
			int vertexCount = (1 + numberOfVertices + numberOfVertices + 1) * 2;
			vertices = new Vector3[vertexCount];
			uv = new Vector2[vertices.Length];
			triangles = new int[numberOfVertices * 3 * 2 + numberOfVertices * 6];
			
			SetVertices();
			SetTriangles();
		}

		private void SetVertices()
		{
			// Right Circle
			int iterStart = 0;
			int iterEnd = numberOfVertices + 1;
			CirclePositions(height / 2f, iterStart, iterEnd);
			
			// Left Circle
			iterStart = iterEnd;
			iterEnd *= 2;
			CirclePositions(-height / 2f, iterStart, iterEnd);

			iterStart = iterEnd;
			int side = 0;
			float angle = 0f;

			for (int i = iterStart; i < vertices.Length; i++)
			{
				float xVal = height / 2f;
				if (side == 1) // Left Side
				{
					xVal *= -1f;
				}
				
				float yVal = Mathf.Sin(angle * Mathf.Deg2Rad) * (width / 2f);
				float zVal = Mathf.Cos(angle * Mathf.Deg2Rad) * (width / 2f);

				vertices[i] = new Vector3(xVal, yVal, zVal);

				++side;

				if (side == 2)
				{
					angle += 360f / numberOfVertices;
					side = 0;
				}
			}
		}

		private void SetTriangles()
		{
			int trianglesCount = 0;
			
			trianglesCount = CirclePolygons(0, trianglesCount);
			trianglesCount = CirclePolygons(numberOfVertices + 1, trianglesCount);

			int pivot = (numberOfVertices + 1) * 2;
			int polygonCount = 0;
			int polygonVertex = 0;

			for (int i = trianglesCount; i < triangles.Length; i++)
			{
				int index = 0;
				
				switch (polygonCount)
				{
					case 0:
					{
						switch (polygonVertex)
						{
							case 0:
							{
								index = pivot;
								break;
							}
							case 1:
							{
								index = pivot + 3;
								break;
							}
							case 2:
							{
								index = pivot + 1;
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
								index = pivot;
								break;
							}
							case 1:
							{
								index = pivot + 2;
								break;
							}
							case 2:
							{
								index = pivot + 3;
								break;
							}
						}
						break;
					}
				}

				triangles[i] = index;

				++polygonVertex;

				if (polygonVertex == 3)
				{
					++polygonCount;
					polygonVertex = 0;
				}

				if (polygonCount == 2)
				{
					pivot += 2;
					polygonCount = 0;
				}
			}
		}

		private void CirclePositions(float xPos, int iterStart, int iterEnd)
		{
			// Point in the center of the circle
			vertices[iterStart] = new Vector3(xPos, 0f, 0f);

			float angle = 0;
			
			// Circle Vertices
			for (int i = iterStart + 1; i < iterEnd; i++)
			{
				float yVal = Mathf.Sin(angle * Mathf.Deg2Rad) * (width / 2f);
				float zVal = Mathf.Cos(angle * Mathf.Deg2Rad) * (width / 2f);

				vertices[i] = new Vector3(xPos, yVal, zVal);
				
				angle += 360f / numberOfVertices;
			}
		}

		private int CirclePolygons(int centerIndex, int trianglesCount)
		{
			int polygonVertex = 0;
			int polygonCount = 0;

			int i;
			
			for (i = trianglesCount; i < trianglesCount + numberOfVertices * 3; i++)
			{
				int index = 0;

				if (centerIndex == 0)
				{
					switch (polygonVertex)
					{
						case 0:
						{
							index = centerIndex;
							break;
						}
						case 1:
						{
							if (polygonCount == numberOfVertices - 1)
							{
								index = centerIndex + 1;
							}
							else
							{
								index = polygonCount + 2 + centerIndex;
							}
							break;
						}
						case 2:
						{
							index = polygonCount + 1 + centerIndex;
							break;
						}
					}
				}
				else
				{
					switch (polygonVertex)
					{
						case 0:
						{
							index = centerIndex;
							break;
						}
						case 1:
						{
							index = polygonCount + 1 + centerIndex;
							break;
						}
						case 2:
						{
							if (polygonCount == numberOfVertices - 1)
							{
								index = centerIndex + 1;
							}
							else
							{
								index = polygonCount + 2 + centerIndex;
							}
							break;
						}
					}
				}

				triangles[i] = index;

				++polygonVertex;

				if (polygonVertex == 3)
				{
					++polygonCount;
					polygonVertex = 0;
				}
			}

			return i;
		}
	}
}
