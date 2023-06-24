using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	[DefaultExecutionOrder(100)]
	[RequireComponent(typeof(Camera))]
	public class SterlingCamera : SingletonGameLoopInstance<SterlingCamera>, IExecuteInEditMode
	{
		#region Fields

		[SerializeField] private CameraData data;

		private CameraControllerBase controller;
		
		public CameraData CameraData => data;

		#endregion

		public override void OnAwake()
		{
			TryGetComponent(out controller);

			if (controller) controller.OnAwake();

			data.camera = GetComponent<Camera>();
		}
		
		public override void OnLateUpdate()
		{
			if (!controller) return;
				
			data.inputDelta = SterlingInputManager.Instance.GetPointerDeltaInput();
			if (data.followPoint) data.followTarget = data.followPoint.position;
				
			controller.OnLateUpdate();
		}

		public void UpdateInEditMode()
		{
			if (!controller)
			{
				TryGetComponent(out controller);
				return;
			}
				
			if (!data.followPoint) return;
				
			data.camera = GetComponent<Camera>();
            data.followTarget = data.followPoint.position;
            controller.OnEditorSetup();
		}
	}
}
