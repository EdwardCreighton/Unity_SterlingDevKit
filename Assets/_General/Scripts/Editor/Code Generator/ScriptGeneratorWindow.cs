using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
	public class ScriptGeneratorWindow : EditorWindow
	{
		#region Fields

		private TextField classNameField;
		private TextField classNamespaceField;
		private EnumField scriptTypeField;

		#endregion
		
		[MenuItem("Window/Sterling/Script Generator")]
		public static void ShowExample()
		{
			EditorWindow window = GetWindow<ScriptGeneratorWindow>();
			
			window.titleContent = new GUIContent("Script Generator");

			window.minSize = new Vector2(200, 100);
			window.maxSize = new Vector2(300, 125);
		}

		private void CreateGUI()
		{
			VisualElement scrollHolder = new ScrollView();
			
			scrollHolder.Add(GetSpace());
			scrollHolder.Add(GetClassNameField());
			scrollHolder.Add(GetClassNamespaceField());
			scrollHolder.Add(GetSpace());
			scrollHolder.Add(GetScriptTypeField());
			scrollHolder.Add(GetSpace());
			scrollHolder.Add(GetCreateScriptButton());
			
			rootVisualElement.Add(scrollHolder);
		}

		private VisualElement GetSpace()
		{
			return new Label("");
		}

		private TextField GetClassNameField()
		{
			classNameField = new TextField("Class Name");
			
			return classNameField;
		}

		private TextField GetClassNamespaceField()
		{
			classNamespaceField = new TextField("Namespace");

			return classNamespaceField;
		}

		private EnumField GetScriptTypeField()
		{
			scriptTypeField = new EnumField("Script Type", ScriptTemplateType.StandardFSM);

			return scriptTypeField;
		}

		private Button GetCreateScriptButton()
		{
			Button createScriptButton = new Button(CreateScript)
			{
				text = "Create Script"
			};

			return createScriptButton;
		}

		private void CreateScript()
		{
			if (string.IsNullOrEmpty(classNameField.value)) return;
			
			// TODO: check naming rules
			
			ScriptGenerator.CreateScript(classNameField.value, classNamespaceField.value, (ScriptTemplateType)scriptTypeField.value);
		}
	}
}
