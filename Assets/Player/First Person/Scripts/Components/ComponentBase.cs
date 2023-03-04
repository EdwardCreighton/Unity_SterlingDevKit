using UnityEngine;

namespace SterlingAssets.Player.POV
{
	public abstract class ComponentBase : MonoBehaviour
	{
		public PlayerData playerData;

		public abstract void OnUpdate();
		public abstract void OnFixedUpdate();
		public abstract void OnLateUpdate();
	}
}
