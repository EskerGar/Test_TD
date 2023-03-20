using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class MoveSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var moveFilter = world.Filter<MoveComponent>().End();
			var movePool = world.GetPool<MoveComponent>();

			foreach (var moveEntity in moveFilter)
			{
				ref var moveComponent = ref movePool.Get(moveEntity);

				var transform = moveComponent.Transform;
				var targetTransform = moveComponent.TargetTransform;
				
				var d = transform.position - targetTransform.position;
				var angle = Mathf.Atan2(d.x, d.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

				transform.position += (targetTransform.position - transform.position).normalized * moveComponent.Speed * Time.deltaTime;
			}
		}
	}
}