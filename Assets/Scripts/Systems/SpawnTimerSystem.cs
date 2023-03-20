using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class SpawnTimerSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var gameFilter = world.Filter<GameComponent>().End();
			var gamePool = world.GetPool<GameComponent>();
			var spawnTimeEventPool = world.GetPool<SpawnTimeEventComponent>();

			var gameEntity = gameFilter.GetRawEntities()[0];
			ref var gameComponent = ref gamePool.Get(gameEntity);
			gameComponent.RemainingSpawnWaveTime -= Time.deltaTime;

			if (gameComponent.RemainingSpawnWaveTime <= 0)
			{
				spawnTimeEventPool.Add(gameEntity);
				gameComponent.RemainingSpawnWaveTime = gameComponent.SpawnWaveTime;
				gameComponent.WaveCount++;
			}
		}
	}
}