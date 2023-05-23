using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SterlingTools
{
	public class EditorWindowWrapper : EditorWindow
	{
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
