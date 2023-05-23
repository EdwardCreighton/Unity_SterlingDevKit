using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
	public class EditorUpdateWindow : EditorWindowWrapper
	{
		private TextElement executablesCount;
		private List<IExecuteInEditMode> executables;
		private ListView executablesListView;

		private const string executablesCountPrefix = "Number of scripts: ";

		[MenuItem("Window/Sterling/ExecuteInEditMode")]
		public static void OpenWindow()
		{
			EditorWindow window = GetWindow<EditorUpdateWindow>();
			
			window.titleContent = new GUIContent("ExecuteInEditMode");
		}

		private void CreateGUI()
		{
			EditorApplication.update += UpdateInEditor;

			VisualElement scrollHolder = new ScrollView();

            executablesCount = new TextElement();
			executablesCount.text = executablesCountPrefix;

			executablesListView = new ListView();

            scrollHolder.Add(GetSpace());
			scrollHolder.Add(executablesCount);
			scrollHolder.Add(GetSpace());
			scrollHolder.Add(executablesListView);
			scrollHolder.Add(GetSpace());
			scrollHolder.Add(GetButton("Update Executables List", SetExecutables));
			
			rootVisualElement.Add(scrollHolder);
		}
		
		private void UpdateInEditor()
		{
			if (executables == null) return;
			
			foreach (IExecuteInEditMode executable in executables)
			{
				if ((MonoBehaviour)executable) executable.UpdateInEditMode();
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

			executablesListView.makeItem = () => new Label();
			executablesListView.bindItem = (item, index) => { ((Label)item).text = executables[index].ToString(); };
			executablesListView.itemsSource = executables;

			executablesCount.text = executablesCountPrefix + executables.Count;
		}

		private void OnDestroy()
		{
			EditorApplication.update -= UpdateInEditor;
		}
	}
}
