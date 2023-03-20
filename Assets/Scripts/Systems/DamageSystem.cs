using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class DamageSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var reachTargetEventFilter = world.Filter<ReachTargetEventComponent>().Inc<BulletComponent>().End();

			if (reachTargetEventFilter.GetEntitiesCount() > 0)
			{
				var enemyPool = world.GetPool<EnemyComponent>();
				var destroyPool = world.GetPool<DestroyEventComponent>();
				var targetEntity = world.Filter<AttackTargetComponent>().End().GetRawEntities()[0];
				var bulletEntity = reachTargetEventFilter.GetRawEntities()[0];
				ref var baseComponent = ref world.GetPool<BaseComponent>().GetRawDenseItems()[1];

				ref var enemyComponent = ref enemyPool.Get(targetEntity);

				enemyComponent.RemainingHealth-= baseComponent.AttackDamage;

				if (enemyComponent.RemainingHealth <= 0)
				{
					destroyPool.Add(targetEntity);
					baseComponent.Money += _configs.Value.KillReward;
					baseComponent.OnMoneyValueChanged?.Invoke(baseComponent.Money);
				}
				
				destroyPool.Add(bulletEntity);
			}
		}
	}
}