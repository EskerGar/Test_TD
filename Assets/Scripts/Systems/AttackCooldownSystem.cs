using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class AttackCooldownSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var cooldownFilter = world.Filter<CooldownComponent>().End();
			var cooldownPool = world.GetPool<CooldownComponent>();

			if (cooldownFilter.GetEntitiesCount() > 0)
			{
				var cooldownEntity = cooldownFilter.GetRawEntities()[0];
				ref var cooldownComponent = ref cooldownPool.Get(cooldownEntity);
				cooldownComponent.RemainingTime -= Time.deltaTime;

				if (cooldownComponent.RemainingTime <= 0)
				{
					cooldownPool.Del(cooldownEntity);
				}
			}
		}
	}
}