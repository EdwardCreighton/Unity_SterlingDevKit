using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingManager : SingletonMonoBehaviour<SterlingManager>
	{
		protected void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
