using SterlingAssets.VehiclePhysX.FourWheel;
using SterlingTools;
using UnityEditor;

public class CarControllerEditorWindow : EditorWindowExtended
{
    public static void Open(CarController carController)
    {
        CarControllerEditorWindow window = GetWindow<CarControllerEditorWindow>("Car Controller Editor");
        window.serializedObject = new SerializedObject(carController);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("wheelComponents");
        DrawProperty(currentProperty);

        ApplyProperties();
    }
}
