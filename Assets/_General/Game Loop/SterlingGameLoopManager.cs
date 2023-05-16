using System.Collections.Generic;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingGameLoopManager : SingletonMono<SterlingGameLoopManager>
	{
		[SerializeField] private List<GameLoopEntity> gameLoopEntities;

		[ContextMenu("Collect Game-Loop Entities")]
		private void CollectGameLoopEntities()
		{
			gameLoopEntities.Clear();

			GameLoopEntity[] tempEntities = FindObjectsOfType<GameLoopEntity>();

			if (tempEntities != null)
				gameLoopEntities.AddRange(tempEntities);
		}

		private void Awake()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				gameLoopEntity.OnAwake();
			}
		}

		private void Start()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				gameLoopEntity.OnStart();
			}
		}

		private void Update()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				gameLoopEntity.OnUpdate();
			}
		}

		private void LateUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				gameLoopEntity.OnLateUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				gameLoopEntity.OnFixedUpdate();
			}
		}
	}
}
