using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.Player.POV
{
	public class PlayerData
	{
		public PlayerComponent playerComponent;
		public Rigidbody rigidbody;

		public List<ContactPoint> collisionPoints;
	}
}
