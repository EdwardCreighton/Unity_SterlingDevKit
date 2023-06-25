using SterlingAssets.VehiclePhysX;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
    [CustomEditor(typeof(WheelComponent))]
	[CanEditMultipleObjects]
	public class Inspector_WheelComponent : Editor
	{
		[SerializeField] private VisualTreeAsset inspectorXML;
		
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement inspector = new VisualElement();

			inspectorXML.CloneTree(inspector);

			return inspector;
		}
	}
}
