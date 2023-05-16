using UnityEngine;

namespace SterlingTools
{
	public abstract class SingletonGameLoopInstance<T> : SterlingAssets.GameLoopEntity where T : SterlingAssets.GameLoopEntity
	{
		private static T ins;

		public static T Instance
		{
			get
			{
				if (!ins)
				{
					ins = FindObjectOfType<T>();

					if (!ins)
					{
						GameObject gameObject = new GameObject(typeof(T).ToString());
						ins = gameObject.AddComponent<T>();
					}
				}

				return ins;
			}
		}
	}
}
