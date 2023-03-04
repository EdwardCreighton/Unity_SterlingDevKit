using SterlingAssets.VehiclePhysX.Car;
using UnityEditor;
using UnityEngine;

namespace SterlingTools.VehiclePhysX.Car
{
	public class CarPhysXTools : Editor
	{
		[MenuItem("GameObject/Sterling/VehiclePhysX/Car", priority = 100)]
		private static void SpawnCarPrototype()
		{
			GameObject carPrototypeGo = new GameObject("Car Prototype");
			Transform carPrototypeTr = carPrototypeGo.transform;

			GameObject selectionGo = Selection.activeGameObject;
			
			carPrototypeTr.parent = selectionGo ? selectionGo.transform : null;
			carPrototypeTr.localPosition = Vector3.zero;
			carPrototypeTr.localRotation = Quaternion.identity;

			carPrototypeGo.AddComponent<CarComponent>();

			GameObject bodyMeshHolderGo = CreateComponent(CarPhysXConfig.BodyMeshHolderName, carPrototypeTr);
			GameObject bodyColliderHolderGo = CreateComponent(CarPhysXConfig.BodyColliderHolderName, carPrototypeTr);
			GameObject wheelMeshHolderGo = CreateComponent(CarPhysXConfig.WheelMeshHolderName, carPrototypeTr);
			GameObject caliperMeshHolderGo = CreateComponent(CarPhysXConfig.CaliperMeshHolderName, carPrototypeTr);
			GameObject wheelColliderHolderGo = CreateComponent(CarPhysXConfig.WheelColliderHolderName, carPrototypeTr);
			GameObject centerOfMassGo = CreateComponent(CarPhysXConfig.CenterOfMassName, carPrototypeTr);

			carPrototypeGo.AddComponent<InputProvider>();
		}

		private static GameObject CreateComponent(string name, Transform parent)
		{
			GameObject componentGo = new GameObject(name);
			Transform componentTr = componentGo.transform;
			
			componentTr.parent = parent;
			componentTr.localPosition = Vector3.zero;
			componentTr.localRotation = Quaternion.identity;
			
			return componentGo;
		}
	}
}
