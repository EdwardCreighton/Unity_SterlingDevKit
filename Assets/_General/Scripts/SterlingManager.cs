using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingManager : SingletonMono<SterlingManager>
	{
		protected void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
