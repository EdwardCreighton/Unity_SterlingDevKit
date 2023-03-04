using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SterlingAssets.VehiclePhysX.Car
{
	public class OverallStatusUI : ControlPanelUIBase
	{
		#region Fields

		[SerializeField] private TextMeshProUGUI engineAngVel;
		[SerializeField] private TextMeshProUGUI engineTorque;
		[SerializeField] private TextMeshProUGUI clutchAngVel;
		[SerializeField] private TextMeshProUGUI clutchTorque;
		[SerializeField] private TextMeshProUGUI gearIndex;
		[SerializeField] private TextMeshProUGUI shaftTorque;
		[SerializeField] private TextMeshProUGUI shaftVelocity;
		[SerializeField] private WheelsInfo wheelsInfos;

		#endregion
		
		protected override void UpdateUI()
		{
			if (engineAngVel) engineAngVel.text = controlPanel.ControlData.engineAngVel.ToString("F1");
			if (engineTorque) engineTorque.text = controlPanel.ControlData.engineTorque.ToString("F1");
			if (clutchAngVel) clutchAngVel.text = controlPanel.ControlData.clutchAngVel.ToString("F1");
			if (clutchTorque) clutchTorque.text = controlPanel.ControlData.clutchTorque.ToString("F1");
			if (gearIndex)
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
				
				gearIndex.text = gear;
			}
			if (shaftTorque) shaftTorque.text = controlPanel.ControlData.shaftTorque.ToString("F1");
			if (shaftVelocity) shaftVelocity.text = controlPanel.ControlData.shaftVelocity.ToString("F1");

			wheelsInfos.UpdateUI(controlPanel.ControlData);
		}

		[Serializable]
		private class WheelsInfo
		{
			[SerializeField] private List<RatioInfo> ratioInfos;

			public void UpdateUI(ControlData controlData)
			{
				for (int i = 0; i < ratioInfos.Count; i++)
				{
					if (i >= controlData.axleInfos.Count) return;
					
					ratioInfos[i].UpdateRatio(controlData.axleInfos[i]);
				}
			}

			[Serializable]
			private class RatioInfo
			{
				[SerializeField] private RectTransform slipTransform;
				[SerializeField] private Image slipImage;
				[Min(0.01f)] [SerializeField] private float slipMaxValue;
				[SerializeField] private RectTransform forceTransform;
				[SerializeField] private Image forceImage;
				[Min(0.01f)] [SerializeField] private float forceMaxValue;
				[SerializeField] private RectTransform velocityTransform;
				[SerializeField] private Image velocityImage;
				[Min(0.01f)] [SerializeField] private float velocityMaxValue;
				[Space]
				[SerializeField] private Gradient gradient;
				[SerializeField] private bool autoCalibrate;

				public void UpdateRatio(ControlData.AxleInfo axleInfo)
				{
					Vector3 scale;
					
					float rightWheelSlip = Mathf.Abs(axleInfo.rightWheelsSlip);
					float leftWheelSlip = Mathf.Abs(axleInfo.leftWheelsSlip);

					if (autoCalibrate)
					{
						float tempMax = Mathf.Abs(rightWheelSlip - leftWheelSlip);

						if (tempMax > slipMaxValue)
						{
							slipMaxValue = tempMax;
						}
						
						slipMaxValue = Mathf.Max(slipMaxValue, 0.001f);
					}

					scale = slipTransform.localScale;
					scale.x = rightWheelSlip - leftWheelSlip;
					scale.x = Mathf.Clamp(scale.x, -slipMaxValue, slipMaxValue);
					scale.x /= slipMaxValue;
					slipTransform.localScale = scale;
					slipImage.color = gradient.Evaluate(Mathf.Abs(scale.x));

					float rightWheelForce = Mathf.Abs(axleInfo.rightWheelsForce);
					float leftWheelForce = Mathf.Abs(axleInfo.leftWheelsForce);
					
					if (autoCalibrate)
					{
						float tempMax = Mathf.Abs(rightWheelForce - leftWheelForce);

						if (tempMax > forceMaxValue)
						{
							forceMaxValue = tempMax;
						}
						
						forceMaxValue = Mathf.Max(forceMaxValue, 0.001f);
					}

					scale = forceTransform.localScale;
					scale.x = rightWheelForce - leftWheelForce;
					scale.x = Mathf.Clamp(scale.x, -forceMaxValue, forceMaxValue);
					scale.x /= forceMaxValue;
					forceTransform.localScale = scale;
					forceImage.color = gradient.Evaluate(Mathf.Abs(scale.x));

					float rightWheelAngVel = Mathf.Abs(axleInfo.rightWheelsAngVel);
					float leftWheelAngVel = Mathf.Abs(axleInfo.leftWheelsAngVel);

					if (autoCalibrate)
					{
						float tempMax = Mathf.Abs(rightWheelAngVel - leftWheelAngVel);

						if (tempMax > velocityMaxValue)
						{
							velocityMaxValue = tempMax;
						}

						velocityMaxValue = Mathf.Max(velocityMaxValue, 0.001f);
					}

					scale = velocityTransform.localScale;
					scale.x = rightWheelAngVel - leftWheelAngVel;
					scale.x = Mathf.Clamp(scale.x, -velocityMaxValue, velocityMaxValue);
					scale.x /= velocityMaxValue;
					velocityTransform.localScale = scale;
					velocityImage.color = gradient.Evaluate(Mathf.Abs(scale.x));
				}
			}
		}
	}
}
