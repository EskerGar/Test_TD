using Components;
using Leopotam.EcsLite;
using UnityEngine.SceneManagement;

namespace Systems
{
	public class RestartGameSystem : IEcsRunSystem
	{

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var reachTargetEventFilter = world.Filter<ReachTargetEventComponent>().Inc<EnemyComponent>().End();

			if (reachTargetEventFilter.GetEntitiesCount() > 0)
			{
				SceneManager.LoadScene("SampleScene");
			}
		}
	}
}