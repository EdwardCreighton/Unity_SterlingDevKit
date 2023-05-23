using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace General
{
	public class PlaneMeshFast : MeshGeneratorFast
	{
		#region Fields

		private int vertexAttributeCount = 4;
		private int vertexCount = 4;

		#endregion
		
		public override void CreateMesh()
		{
			AllocateMeshData("Procedural Plane Fast Gen");
			SetPlaneMeshData();
			ApplyMeshData();
		}

		private void SetPlaneMeshData()
		{
			Mesh.MeshData meshData = meshDataArray[0];

			var vertexAttributes = new NativeArray<VertexAttributeDescriptor>(vertexAttributeCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);

			vertexAttributes[0] = new VertexAttributeDescriptor(VertexAttribute.Position, dimension: 3, stream: 0);
			vertexAttributes[1] = new VertexAttributeDescriptor(VertexAttribute.Normal, dimension: 3, stream: 1);
			vertexAttributes[2] = new VertexAttributeDescriptor(VertexAttribute.Tangent, dimension: 4, stream: 2);
			vertexAttributes[3] = new VertexAttributeDescriptor(VertexAttribute.TexCoord0, dimension: 2, stream: 3);
			
			meshData.SetVertexBufferParams(vertexCount, vertexAttributes);
			vertexAttributes.Dispose();
			
			
		}
	}
}
