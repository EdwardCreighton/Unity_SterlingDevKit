using System;
using UnityEditor;

namespace SterlingTools
{
	public static class ScriptGenerator
	{
		public static void CreateScript(string className, string classNamespace, ScriptTemplateType scriptType)
		{
			switch (scriptType)
			{
				case ScriptTemplateType.StandardFSM:
				{
					CreateFSMScript(className, classNamespace);
					break;
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(scriptType), scriptType, null);
			}
		}
		
		private static void CreateFSMScript(string className, string classNamespace)
		{
			string path;
			string script;
			
			script = FSMTemplates.GetFSMTemplate(className, classNamespace);

			path = EditorUtility.SaveFilePanelInProject("Save New FSM Script", $"{className}_FSMController.cs", "cs", "Message");

			if (path.Length == 0) return;
			
			System.IO.File.WriteAllText(path, script);

			script = FSMTemplates.GetAbstractStateTemplate(className, classNamespace);
			
			path = EditorUtility.SaveFilePanelInProject("Save New Abstract State", $"{className}_FSM_AbstractState.cs", "cs", "Message");

			if (path.Length == 0) return;
			
			System.IO.File.WriteAllText(path, script);

			AssetDatabase.Refresh();
		}
	}
}
