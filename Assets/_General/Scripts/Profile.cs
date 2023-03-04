using UnityEngine;

namespace SterlingTools
{
	public abstract class Profile : ScriptableObject
	{
		#region Fields

		private bool onceFlag;

		#endregion

		protected void ThrowException()
		{
			if (onceFlag) return;

			Debug.LogWarning($"Missing reference to {this}.");
			onceFlag = true;
		}
	}
}
