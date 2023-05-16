using System;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	[DefaultExecutionOrder(100)]
	[RequireComponent(typeof(Camera))]
	public class SterlingCamera : SingletonMono<SterlingCamera>, IExecuteInEditMode
	{
		#region Fields

		[SerializeField] private CameraData data;

		private CameraControllerBase controller;
		
		public CameraData CameraData => data;

		#endregion

		private void Awake()
		{
			TryGetComponent(out controller);

			if (controller) controller.OnAwake();

			data.camera = GetComponent<Camera>();
		}
		
		private void LateUpdate()
		{
			if (!controller) return;
				
			data.inputDelta = SterlingInputManager.Instance.GetPointerDeltaInput();
			if (data.followPoint) data.followTarget = data.followPoint.position;
				
			controller.OnLateUpdate();
		}

		public void RequestCameraTask(Action<CameraData> task)
		{
			controller.SetTask(task);
		}

		[ContextMenu("Run Test Task")]
		private void RunTask()
		{
			RequestCameraTask(CameraCutscenes.TestTask);
		}

		public void ExecuteInEditMode()
		{
			if (!controller)
			{
				TryGetComponent(out controller);
				return;
			}
				
			if (!data.followPoint) return;
				
			data.camera = GetComponent<Camera>();
			controller.OnEditorSetup();
		}
	}
}
