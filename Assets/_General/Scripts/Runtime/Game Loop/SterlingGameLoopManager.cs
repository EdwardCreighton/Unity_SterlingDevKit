using System.Collections.Generic;
using SterlingTools;
using UnityEngine;

namespace SterlingAssets
{
	public class SterlingGameLoopManager : SingletonMonoBehaviour<SterlingGameLoopManager>
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
				if (gameLoopEntity.enabled) gameLoopEntity.OnAwake();
			}
		}

		private void Start()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				if (gameLoopEntity.enabled) gameLoopEntity.OnStart();
			}
		}

		private void Update()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				if (gameLoopEntity.enabled) gameLoopEntity.OnUpdate();
			}
		}

		private void LateUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				if (gameLoopEntity.enabled) gameLoopEntity.OnLateUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (gameLoopEntities == null) return;

			foreach (GameLoopEntity gameLoopEntity in gameLoopEntities)
			{
				if (gameLoopEntity.enabled) gameLoopEntity.OnFixedUpdate();
			}
		}
	}
}