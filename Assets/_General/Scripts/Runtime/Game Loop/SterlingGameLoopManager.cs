using System.Collections.Generic;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingGameLoopManager : SingletonMonoBehaviour<SterlingGameLoopManager>
	{
		[SerializeField] private List<GameLoopEntity> gameLoopEntities;

		// TODO: Move this method to custom editor
		[ContextMenu("Collect Game-Loop Entities")]
		private void CollectGameLoopEntities()
		{
			if (gameLoopEntities == null)
			{
				gameLoopEntities = new List<GameLoopEntity>();
			}
			else
			{
                gameLoopEntities.Clear();
            }

			GameLoopEntity[] tempEntities = FindObjectsOfType<GameLoopEntity>();

			if (tempEntities != null)
				gameLoopEntities.AddRange(tempEntities);
		}

		private void Awake()
		{
			gameLoopEntities?.CallGameLoopEntity((gameLoopEntity) => gameLoopEntity.OnAwake());
        }

		private void Start()
		{
			gameLoopEntities?.CallGameLoopEntity((gameLoopEntity) => gameLoopEntity.OnStart());
        }

		private void Update()
		{
			gameLoopEntities?.CallGameLoopEntity((gameLoopEntity) => gameLoopEntity.OnUpdate());
        }

		private void LateUpdate()
		{
			gameLoopEntities?.CallGameLoopEntity((gameLoopEntity) => gameLoopEntity.OnLateUpdate());
		}

		private void FixedUpdate()
		{
			gameLoopEntities?.CallGameLoopEntity((gameLoopEntity) => gameLoopEntity.OnFixedUpdate());
		}
	}
}
