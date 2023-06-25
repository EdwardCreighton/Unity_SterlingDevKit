using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
    public class EditorWindowExtended : EditorWindow
	{
		protected SerializedObject serializedObject;
		protected SerializedProperty currentProperty;

		protected void DrawProperty(string propertyName, string label = null)
		{
			SerializedProperty property = serializedObject.FindProperty(propertyName);
            DrawProperty(property, label);
        }

        protected void DrawProperty(SerializedProperty property, string label = null)
        {
            if (property == null)
            {
                Debug.LogError("Property does not exist!");
                return;
            }

            if (label != null)
            {
                EditorGUILayout.PropertyField(property, new GUIContent(label), true);
            }
            else
            {
                EditorGUILayout.PropertyField(property, true);
            }
        }

        protected void ApplyProperties()
        {
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawButton(string buttonName, Action onClickAction)
        {
            if (GUILayout.Button(buttonName)) onClickAction.Invoke();
        }

        protected void DrawSpace(float val = 10f)
        {
            EditorGUILayout.Space(val);
        }

        protected VisualElement GetSpace(int size = 1)
		{
			string temp = String.Empty;

			size = Mathf.Max(size, 1);

			for (int i = 0; i < size; i++)
			{
				temp += "\n ";
			}
			
			return new Label(temp);
		}
		
		protected Button GetButton(string buttonTitle, Action action)
		{
			Button button = new Button(action)
			{
				text = buttonTitle
			};

			return button;
		}

        private TextField GetTextField(string label)
        {
            return new TextField(label);
        }
    }
}
