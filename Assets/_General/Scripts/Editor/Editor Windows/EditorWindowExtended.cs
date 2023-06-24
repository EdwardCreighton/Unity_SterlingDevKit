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

        protected void DrawProperty(SerializedProperty property)
        {
            if (property == null)
            {
                Debug.LogError($"Can't draw property because it's null!");
                return;
            }

            EditorGUILayout.PropertyField(property, true);
        }

        protected void DrawProperty(SerializedProperty property, bool drawChildren)
		{
			if (property == null)
			{
				Debug.LogError($"Can't draw property because it's null!");
				return;
			}

			string lastPropertyPath = string.Empty;

            foreach (SerializedProperty internalProperty in property)
			{
				if (internalProperty.isArray && internalProperty.propertyType == SerializedPropertyType.Generic)
				{
					EditorGUILayout.BeginHorizontal();
					internalProperty.isExpanded = EditorGUILayout.Foldout(internalProperty.isExpanded, internalProperty.displayName);
					EditorGUILayout.EndHorizontal();

					if (internalProperty.isExpanded)
					{
						EditorGUI.indentLevel++;
						DrawProperty(internalProperty, drawChildren);
						EditorGUI.indentLevel--;
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(lastPropertyPath) && internalProperty.propertyPath.Contains(lastPropertyPath)) continue;

					lastPropertyPath = internalProperty.propertyPath;
					EditorGUILayout.PropertyField(internalProperty, drawChildren);
				}
			}
		}

        protected void ApplyProperties()
        {
            serializedObject.ApplyModifiedProperties();
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
