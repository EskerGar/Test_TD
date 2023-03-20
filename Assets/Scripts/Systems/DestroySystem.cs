using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class DestroySystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<EnemyPool> _enemyPool;
		private readonly EcsCustomInject<BulletPool> _bulletPool;

		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var destroyEventFilter = world.Filter<DestroyEventComponent>().End();
			var enemyPool = world.GetPool<EnemyComponent>();
			var bulletPool = world.GetPool<BulletComponent>();

			foreach (var destroyEntity in destroyEventFilter)
			{
				if (enemyPool.Has(destroyEntity))
				{
					var enemyComponent = enemyPool.Get(destroyEntity);
					_enemyPool.Value.Put(enemyComponent.EnemyView);
				}
				else if (bulletPool.Has(destroyEntity))
				{
					var bulletComponent = bulletPool.Get(destroyEntity);
					_bulletPool.Value.Put(bulletComponent.BulletView);
				}
				
				world.DelEntity(destroyEntity);
			}
		}
	}
}