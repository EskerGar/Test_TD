using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class FindTargetSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var targetFilter = world.Filter<AttackTargetComponent>().End();

			if (targetFilter.GetEntitiesCount() == 0)
			{
				var enemyFilter = world.Filter<EnemyComponent>().End();
				var enemyPool = world.GetPool<EnemyComponent>();
				var attackTargetPool = world.GetPool<AttackTargetComponent>();
				var baseComponent = world.GetPool<BaseComponent>().GetRawDenseItems()[1];
				var distance = float.MaxValue;
				var nearestEnemyEntity = -1;

				foreach (var enemyEntity in enemyFilter)
				{
					var enemyComponent = enemyPool.Get(enemyEntity);
					var enemyPosition = enemyComponent.EnemyView.transform.position;

					var currentDistance = (enemyPosition - baseComponent.BaseView.transform.position).sqrMagnitude;

					if (currentDistance < distance)
					{
						distance = currentDistance;
						nearestEnemyEntity = enemyEntity;
					}
				}

				if (nearestEnemyEntity != -1 && distance <= Mathf.Pow(baseComponent.AttackRange, 2))
				{
					attackTargetPool.Add(nearestEnemyEntity);
				}
			}
		}
	}
}