using UnityEngine;

namespace SterlingTools
{
	public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour, IDontDestroyOnLoad
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
					
					if (ins.DontDestroyOnLoadValue) DontDestroyOnLoad(ins.gameObject);
				}

				return ins;
			}
		}
	}

	public interface IDontDestroyOnLoad
	{
		public bool DontDestroyOnLoadValue { get; }
	}
}
