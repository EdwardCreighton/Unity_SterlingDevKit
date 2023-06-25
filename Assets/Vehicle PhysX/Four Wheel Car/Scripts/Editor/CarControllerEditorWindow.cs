using SterlingAssets.VehiclePhysX;
using SterlingAssets.VehiclePhysX.FourWheel;
using SterlingTools;
using UnityEditor;
using UnityEngine;

public class CarControllerEditorWindow : EditorWindowExtended
{
    private enum Wheel
    {
        FL, FR, RL, RR
    }

    private Wheel selectedWheel;

    private const string WheelCollidersHolderName = "Wheel Colliders";

    public static void Open(CarController carController)
    {
        CarControllerEditorWindow window = GetWindow<CarControllerEditorWindow>("Car Controller Editor");
        window.serializedObject = new SerializedObject(carController);
    }

    private void OnGUI()
    {
        DrawCarPropertiesSection();

        DrawSpace(20f);

        DrawWheelsPropertiesSection();

        DrawSpace(20f);

        DrawWheelsAdditionlPropertiesSection();

        DrawSpace(20f);

        DrawWheelCollidersPropertiesSection();

        ApplyProperties();
    }

    private void DrawWheelCollidersPropertiesSection()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.MaxWidth(350f));
        DrawProperty("wheelComponentFL");
        DrawProperty("wheelComponentFR");
        DrawProperty("wheelComponentRL");
        DrawProperty("wheelComponentRR");
        DrawSpace();
        DrawButton("Initialize Wheel Components", InitWheelComponents);
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void DrawWheelsAdditionlPropertiesSection()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.MaxWidth(350f));
        DrawProperty("suspensionProfile");
        DrawProperty("frictionProfile");
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void DrawWheelsPropertiesSection()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical("box", GUILayout.MaxWidth(150f));
        SelectWheel();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box", GUILayout.MaxWidth(300f));
        DrawSelectedWheelProperties();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void DrawCarPropertiesSection()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.MaxWidth(350f));
        DrawProperty("vehicleRigidbody");
        DrawProperty("vehicleColliders");
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void SelectWheel()
    {
        DrawButton("Front Left wheel", () => selectedWheel = Wheel.FL);
        DrawButton("Front Right wheel", () => selectedWheel = Wheel.FR);
        DrawButton("Rear Left wheel", () => selectedWheel = Wheel.RL);
        DrawButton("Rear Right wheel", () => selectedWheel = Wheel.RR);
    }

    private void DrawSelectedWheelProperties()
    {
        switch (selectedWheel)
        {
            case Wheel.FL:
                GUILayout.Label("Front Left Wheel settings:");
                DrawProperty("tyreFL", "Tyre");
                DrawProperty("hubcapFL", "Hubcap");
                DrawProperty("caliperFL", "Caliper");
                DrawProperty("brakeDiskFL", "Brake Disk");
                break;
            case Wheel.FR:
                GUILayout.Label("Front Right Wheel settings:");
                DrawProperty("tyreFR", "Tyre");
                DrawProperty("hubcapFR", "Hubcap");
                DrawProperty("caliperFR", "Caliper");
                DrawProperty("brakeDiskFR", "Brake Disk");
                break;
            case Wheel.RL:
                GUILayout.Label("Rear Left Wheel settings:");
                DrawProperty("tyreRL", "Tyre");
                //EditorGUILayout.HelpBox(new GUIContent(), MessageType.Error);
                DrawProperty("hubcapRL", "Hubcap");
                DrawProperty("caliperRL", "Caliper");
                DrawProperty("brakeDiskRL", "Brake Disk");
                break;
            case Wheel.RR:
                GUILayout.Label("Rear Right Wheel settings:");
                DrawProperty("tyreRR", "Tyre");
                DrawProperty("hubcapRR", "Hubcap");
                DrawProperty("caliperRR", "Caliper");
                DrawProperty("brakeDiskRR", "Brake Disk");
                break;
            default:
                break;
        }
    }

    private void InitWheelComponents()
    {
        GameObject carController = (GameObject)serializedObject.context;

        Transform wheelCollidersHolder = carController.transform.Find(WheelCollidersHolderName);

        if (!wheelCollidersHolder)
        {
            wheelCollidersHolder = new GameObject(WheelCollidersHolderName).transform;
            wheelCollidersHolder.transform.parent = carController.transform;
            wheelCollidersHolder.localPosition = Vector3.zero;
            wheelCollidersHolder.localRotation = Quaternion.identity;
            wheelCollidersHolder.localScale = Vector3.one;
        }
        else
        {
            int childCount = wheelCollidersHolder.childCount;

            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(wheelCollidersHolder.GetChild(0).gameObject);
            }
        }

        

    }

    private void CreateWheelCollider(string colliderName,
        Transform wheelCollidersHolder,
        Transform tyreMesh,
        Transform hubcapMesh,
        Transform caliperMesh,
        Transform brakeDiskMesh)
    {
        GameObject wheelColliderGo = new GameObject(colliderName);
        Transform wheelColliderTr = wheelColliderGo.transform;

        wheelColliderTr.position = tyreMesh.position;
        wheelColliderTr.parent = wheelCollidersHolder;
        wheelColliderTr.localScale = Vector3.one;
        wheelColliderTr.localRotation = Quaternion.identity;

        WheelComponent_v2 wheelComponent = wheelColliderGo.GetComponent<WheelComponent_v2>();
        wheelComponent.TyreTransform = tyreMesh;
        wheelComponent.HubcapTransform = hubcapMesh;
        wheelComponent.CaliperTransform = caliperMesh;
        wheelComponent.BrakeDiskTransform = brakeDiskMesh;
    }
}
