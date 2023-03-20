using Components;
using Leopotam.EcsLite;

namespace Systems
{
	public class CheckReachTargetSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var moveFilter = world.Filter<MoveComponent>().End();
			var movePool = world.GetPool<MoveComponent>();
			var reachTargetEventPool = world.GetPool<ReachTargetEventComponent>();
			
			foreach (var moveEntity in moveFilter)
			{
				ref var moveComponent = ref movePool.Get(moveEntity);

				var transform = moveComponent.Transform;
				var targetTransform = moveComponent.TargetTransform;
				var distance = (targetTransform.position - transform.position).sqrMagnitude;

				if (distance <= .1f)
				{
					reachTargetEventPool.Add(moveEntity);
				}
			}
		}
	}
}