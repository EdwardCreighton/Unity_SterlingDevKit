using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
	public class EditorUpdateWindow : EditorWindowWrapper
	{
		private List<IExecuteInEditMode> executables;

		[MenuItem("Window/Sterling/ExecuteInEditMode")]
		public static void OpenWindow()
		{
			EditorWindow window = GetWindow<EditorUpdateWindow>();
			
			window.titleContent = new GUIContent("ExecuteInEditMode");

			window.minSize = new Vector2(200, 100);
			window.maxSize = new Vector2(300, 125);
		}

		private void CreateGUI()
		{
			EditorApplication.update += UpdateInEditor;

			VisualElement scrollHolder = new ScrollView();

			scrollHolder.Add(GetSpace());
			scrollHolder.Add(GetButton("Update Executables List", SetExecutables));
			
			rootVisualElement.Add(scrollHolder);
		}
		
		private void UpdateInEditor()
		{
			if (executables == null) return;
			
			foreach (IExecuteInEditMode executable in executables)
			{
				executable.UpdateInEditMode();
			}
		}

		private void SetExecutables()
		{
			Transform[] allScripts = FindObjectsOfType<Transform>();

			if (executables != null)
			{
				executables.Clear();
			}
			else
			{
				executables = new List<IExecuteInEditMode>();
			}

			foreach (Transform transform in allScripts)
			{
				if (!transform.TryGetComponent(out IExecuteInEditMode executable)) continue;
				
				if (executables.Contains(executable)) continue;
					
				executables.Add(executable);
			}
		}

		private void OnDestroy()
		{
			EditorApplication.update -= UpdateInEditor;
		}
	}
}
