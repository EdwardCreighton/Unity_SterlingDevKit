using System.Collections.Generic;
using UnityEngine;

namespace SterlingTools
{
	[ExecuteInEditMode]
	public class SterlingExecuteInEditModeManager : MonoBehaviour
	{
		[SerializeField] private int executablesCount;
		private List<IExecuteInEditMode> executables;

		[ContextMenu("Update Executables List")]
		private void UpdateExecutables()
		{
			MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

			executables = new List<IExecuteInEditMode>();
			
			foreach (MonoBehaviour behaviour in allScripts)
			{
				if (!behaviour.TryGetComponent(out IExecuteInEditMode executable)) continue;
				
				if (executables.Contains(executable)) continue;
					
				executables.Add(executable);
			}
		}
		
		private void OnValidate()
		{
			UpdateExecutables();
		}

		private void Update()
		{
			if (Application.isPlaying) return;

			if (executables == null)
			{
				executablesCount = -1;
				return;
			}

			executablesCount = executables.Count;

			foreach (var executable in executables)
			{
				executable.UpdateInEditMode();
			}
		}
	}
}
