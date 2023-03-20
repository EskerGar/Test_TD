using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class AttackSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<BulletPool> _bulletPool;
		private readonly EcsCustomInject<Configs> _configs;
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var targetFilter = world.Filter<AttackTargetComponent>().End();
			var cooldownFilter = world.Filter<CooldownComponent>().End();

			if (targetFilter.GetEntitiesCount() > 0 && cooldownFilter.GetEntitiesCount() == 0)
			{
				var enemyPool = world.GetPool<EnemyComponent>();
				var bulletPool = world.GetPool<BulletComponent>();
				var movePool = world.GetPool<MoveComponent>();
				var basePool = world.GetPool<BaseComponent>();
				var cooldownPool = world.GetPool<CooldownComponent>();
				var baseEntity = world.Filter<BaseComponent>().End().GetRawEntities()[0];
				var bulletEntity = world.NewEntity();
				var targetEntity = targetFilter.GetRawEntities()[0];
				var bulletView = _bulletPool.Value.Get();
				var baseComponent = basePool.Get(baseEntity);
				bulletView.transform.position = baseComponent.BaseView.transform.position;

				bulletPool.Add(bulletEntity);
				movePool.Add(bulletEntity);
				cooldownPool.Add(baseEntity);

				var enemyComponent = enemyPool.Get(targetEntity);

				ref var bulletComponent = ref bulletPool.Get(bulletEntity);
				bulletComponent.BulletView = bulletView;

				ref var moveComponent = ref movePool.Get(bulletEntity);
				moveComponent.Speed = _configs.Value.BulletSpeed;
				moveComponent.TargetTransform = enemyComponent.EnemyView.transform;
				moveComponent.Transform = bulletView.transform;

				ref var cooldownComponent = ref cooldownPool.Get(baseEntity);
				cooldownComponent.RemainingTime = baseComponent.AttackSpeed;
			}
		}
	}
}