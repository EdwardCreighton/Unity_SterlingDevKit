using UnityEngine;

namespace SterlingAssets
{
	public class GameLoopEntity : MonoBehaviour
	{
		public virtual void OnAwake() {}
		public virtual void OnStart() {}
		public virtual void OnUpdate() {}
		public virtual void OnLateUpdate() {}
		public virtual void OnFixedUpdate() {}
	}
}
