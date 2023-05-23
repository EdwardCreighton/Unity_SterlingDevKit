using System;
using UnityEngine;

namespace SterlingAssets
{
	public abstract class CameraControllerBase : MonoBehaviour
	{
		protected CameraData cameraData;

		protected bool IsBusy;

		protected Action<CameraData> TaskSequence;
		
		public abstract void OnAwake();

		public abstract void OnUpdate();

		public abstract void OnLateUpdate();

		public abstract void OnEditorSetup();

		public void SetTask(Action<CameraData> task)
		{
			if (IsBusy) return;

			IsBusy = true;

			TaskSequence = task;
		}
	}
}
