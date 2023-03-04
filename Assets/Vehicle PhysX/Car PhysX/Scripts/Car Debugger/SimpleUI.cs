using TMPro;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.Car
{
	public class SimpleUI : ControlPanelUIBase
	{
		#region Fields

		[SerializeField] private RectTransform rpmValue;
		[SerializeField] private TextMeshProUGUI gearValue;
		[SerializeField] private TextMeshProUGUI speedValue;

		#endregion
		
		protected override void UpdateUI()
		{
			if (rpmValue) UpdateRpm();
			if (speedValue) UpdateSpeed();
			if (gearValue) UpdateGear();
		}

		private void UpdateRpm()
		{
			Vector3 scale = rpmValue.localScale;
			scale.x = Mathf.Clamp01(controlPanel.ControlData.engineRpm / controlPanel.ControlData.maxRpm);
			rpmValue.localScale = scale;
		}

		private void UpdateSpeed()
		{
			speedValue.text = controlPanel.ControlData.carSpeed.ToString("0");
		}

		private void UpdateGear()
		{
			string gear;

			switch (controlPanel.ControlData.currentGearIndex)
			{
				case -1:
				{
					gear = "R";
					break;
				}
				case 0:
				{
					gear = "N";
					break;
				}
				default:
				{
					gear = controlPanel.ControlData.currentGearIndex.ToString();
					break;
				}
			}

			gearValue.text = gear;
		}
	}
}
