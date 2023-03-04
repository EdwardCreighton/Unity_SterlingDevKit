using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public abstract class ControlPanelUIBase : MonoBehaviour
	{
		[SerializeField] protected ControlPanel controlPanel;
		
		private void Start()
		{
			controlPanel.OnUpdateUI += UpdateUI;
		}

		protected abstract void UpdateUI();
	}
}
