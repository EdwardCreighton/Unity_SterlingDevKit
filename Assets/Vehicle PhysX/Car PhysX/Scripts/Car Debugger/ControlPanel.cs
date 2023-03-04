using System;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public class ControlPanel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private CarComponent carComponent;
		[SerializeField] private ControlData controlData;
		
		#endregion

		public ControlData ControlData => controlData;

		public event Action OnUpdateUI;

		private void Update()
		{
			if (carComponent) carComponent.GetControlData(controlData);
			
			OnUpdateUI?.Invoke();
		}
	}
}
