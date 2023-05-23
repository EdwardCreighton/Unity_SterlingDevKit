using System;
using SterlingTools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SterlingAssets
{
	public class SterlingInputManager : SingletonMonoBehaviour<SterlingInputManager>
	{
		#region Fields

		private SterlingInput sterlingInput;
		private PlayerInput playerInput;

		#endregion

		#region Getters

		public SterlingInput.CarActions CarActions => sterlingInput.Car;
		public SterlingInput.PlayerActions PlayerActions => sterlingInput.Player;
		public SterlingInput.UIActions UIActions => sterlingInput.UI;

		#endregion

		private void Awake()
		{
			sterlingInput = new SterlingInput();
			playerInput = GetComponent<PlayerInput>();
			playerInput.actions = sterlingInput.asset;
		}

		public Vector2 GetPointerDeltaInput()
		{
			string currentActionMapName = playerInput.currentActionMap.name;
			
			if (currentActionMapName == GetActionMapName(PlayerActions.ToString()))
			{
				return PlayerActions.Look.ReadValue<Vector2>();
			}

			if (currentActionMapName == GetActionMapName(CarActions.ToString()))
			{
				return CarActions.LookCamera.ReadValue<Vector2>();
			}

			if (currentActionMapName == GetActionMapName(UIActions.ToString()))
			{
				return UIActions.Navigate.ReadValue<Vector2>();
			}

			return Vector2.zero;
		}

		private string GetActionMapName(string fullName)
		{
			int concatIndex = fullName.IndexOf('+');

			string temp = concatIndex >= 0 ? fullName.Substring(concatIndex + 1) : fullName;

			// ...Actions
			return temp[new Range(0, temp.Length - 7)];
		}
	}
}
