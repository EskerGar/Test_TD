using Components;
using Leopotam.EcsLite;

namespace Systems
{
	public class EventsDeletingSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var destroyFilter = world.Filter<DestroyEventComponent>().End();
			var reachTargetFilter = world.Filter<ReachTargetEventComponent>().End();
			var spawnTimeFilter = world.Filter<SpawnTimeEventComponent>().End();
			var upgradeFilter = world.Filter<UpgradeEventComponent>().End();
			
			var destroyPool = world.GetPool<DestroyEventComponent>();
			var reachTargetPool = world.GetPool<ReachTargetEventComponent>();
			var spawnTimePool = world.GetPool<SpawnTimeEventComponent>();
			var upgradePool = world.GetPool<UpgradeEventComponent>();
			
			DelEvent(destroyFilter, destroyPool);
			DelEvent(reachTargetFilter, reachTargetPool);
			DelEvent(spawnTimeFilter, spawnTimePool);
			DelEvent(upgradeFilter, upgradePool);
		}
		
		private void DelEvent<T>(EcsFilter filter, EcsPool<T> pool) 
			where T : struct
		{
			foreach (var entity in filter)
			{
				pool.Del(entity);
			}
		}
	}
}