using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingManager : SingletonMono<SterlingManager>, IDontDestroyOnLoad
	{
		public bool DontDestroyOnLoadValue => true;
		
		protected void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
