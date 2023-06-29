using System;
using System.Collections.Generic;
using SterlingAssets;

namespace SterlingTools
{
	public static class ListExtensions
	{
		public static void CallGameLoopEntity(this List<GameLoopEntity> gameLoopEntitiesList, Action<GameLoopEntity> gameLoopEntityAction)
		{
			int listSize = gameLoopEntitiesList.Count;

			for (int i = 0; i < listSize; i++)
			{
				if (!gameLoopEntitiesList[i])
					continue;
				
				if (!gameLoopEntitiesList[i].gameObject.activeInHierarchy || !gameLoopEntitiesList[i].BehaviourEnabled)
					continue;

				gameLoopEntityAction.Invoke(gameLoopEntitiesList[i]);
			}
		}
	}
}
