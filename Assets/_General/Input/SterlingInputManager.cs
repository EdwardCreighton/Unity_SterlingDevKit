using System;
using System.Collections.Generic;
using SterlingTools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SterlingAssets
{
	public class SterlingInputManager : SingletonMono<SterlingInputManager>, IDontDestroyOnLoad
	{
		#region Fields

		[SerializeField] private List<string> names;

		private SterlingInput sterlingInput;
		private PlayerInput playerInput;

		#endregion

		#region Getters
		
		public bool DontDestroyOnLoadValue => true;

		public SterlingInput SterlingInput => sterlingInput;
		public SterlingInput.CarActions CarActions => sterlingInput.Car;
		public SterlingInput.PlayerActions PlayerActions => sterlingInput.Player;
		public SterlingInput.UIActions UIActions => sterlingInput.UI;

		#endregion

		private void Awake()
		{
			
		}

		private void OnValidate()
		{
			playerInput = GetComponent<PlayerInput>();
			
			names.Clear();

			foreach (var actionMap in playerInput.actions.actionMaps)
			{
				names.Add(actionMap.name);
			}
		}

		public Vector2 GetPointerDeltaInput()
		{
			return Vector2.zero;

			/*switch (activeActionMap)
			{
				case ActionMap.Car:
				{
					return CarActions.LookCamera.ReadValue<Vector2>();
				}
				case ActionMap.Player:
				{
					return PlayerActions.Look.ReadValue<Vector2>();
				}
				case ActionMap.UI:
				{
					return UIActions.Navigate.ReadValue<Vector2>();
				}
				default:
				{
					Debug.LogError($"Missing Action Map description in {typeof(ActionMap)}.");
					return Vector2.zero;
				}
			}*/
		}
	}
}
