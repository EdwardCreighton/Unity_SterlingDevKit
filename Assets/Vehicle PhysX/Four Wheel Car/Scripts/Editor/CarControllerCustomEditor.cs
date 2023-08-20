using UnityEditor;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.FourWheel
{
	[CustomEditor(typeof(CarController))]
	public class CarControllerCustomEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Open Car Controller Editor"))
			{
				CarControllerEditorWindow.Open((CarController)target);
			}
		}
	}
}
