using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public class EnemySpawnSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<EnemyPool> _enemyPool;
		private readonly EcsCustomInject<Configs> _configs;

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var spawnTimeFilter = world.Filter<SpawnTimeEventComponent>().End();
			
			if (spawnTimeFilter.GetEntitiesCount() > 0)
			{
				var enemyPool = world.GetPool<EnemyComponent>();
				var movePool = world.GetPool<MoveComponent>();
				var gamePool = world.GetPool<GameComponent>();
				var gameEntity = spawnTimeFilter.GetRawEntities()[0];
				var baseComponent = world.GetPool<BaseComponent>().GetRawDenseItems()[1];

				var gameComponent = gamePool.Get(gameEntity);

				for (int i = 0; i < gameComponent.WaveCount; i++)
				{
					var enemyEntity = world.NewEntity();
					var enemyView = _enemyPool.Value.Get();
					var randAngle = Random.Range(0, 2 * Mathf.PI);
					var randRange = Random.Range(baseComponent.AttackRange, _configs.Value.MaxSpawnRange);
					var coord = new Vector3(randRange * Mathf.Cos(randAngle), randRange * Mathf.Sin(randAngle));
					
					enemyView.transform.position = coord;

					enemyPool.Add(enemyEntity);
					movePool.Add(enemyEntity);

					ref var enemyComponent = ref enemyPool.Get(enemyEntity);
					enemyComponent.EnemyView = enemyView;
					enemyComponent.RemainingHealth = _configs.Value.EnemyHealth;

					ref var moveComponent = ref movePool.Get(enemyEntity);
					moveComponent.Speed = _configs.Value.EnemyMoveSpeed;
					moveComponent.TargetTransform = baseComponent.BaseView.transform;
					moveComponent.Transform = enemyComponent.EnemyView.transform;
				}
			}
		}
	}
}