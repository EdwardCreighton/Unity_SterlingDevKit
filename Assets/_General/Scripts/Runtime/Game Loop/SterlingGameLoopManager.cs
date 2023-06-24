using System.Collections.Generic;
using SterlingTools;
using UnityEngine;
using System.Linq;

namespace SterlingAssets
{
	public class SterlingGameLoopManager : SingletonMonoBehaviour<SterlingGameLoopManager>
	{
		[SerializeField] private List<GameLoopEntity> gameLoopEntities;

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
			if (gameLoopEntities == null) return;
            
			foreach (var gameLoopEntity in gameLoopEntities.Where(gameLoopEntity => gameLoopEntity.BehaviourEnabled))
            {
                gameLoopEntity.OnAwake();
            }
        }

		private void Start()
		{
			if (gameLoopEntities == null) return;
            
			foreach (var gameLoopEntity in gameLoopEntities.Where(gameLoopEntity => gameLoopEntity.BehaviourEnabled))
            {
                gameLoopEntity.OnStart();
            }
        }

		private void Update()
		{
			if (gameLoopEntities == null) return;
            
			foreach (var gameLoopEntity in gameLoopEntities.Where(gameLoopEntity => gameLoopEntity.BehaviourEnabled))
            {
                gameLoopEntity.OnUpdate();
            }
        }

		private void LateUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (var gameLoopEntity in gameLoopEntities.Where(gameLoopEntity => gameLoopEntity.BehaviourEnabled))
            {
                gameLoopEntity.OnLateUpdate();
            }
		}

		private void FixedUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (var gameLoopEntity in gameLoopEntities.Where(gameLoopEntity => gameLoopEntity.BehaviourEnabled))
            {
                gameLoopEntity.OnFixedUpdate();
            }
		}
	}
}
