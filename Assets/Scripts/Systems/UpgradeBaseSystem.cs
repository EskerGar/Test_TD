using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class UpgradeBaseSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<Configs> _configs;

		private delegate void ActionRef<T1>(ref T1 arg1);
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var upgradeEventFilter = world.Filter<UpgradeEventComponent>().End();

			if (upgradeEventFilter.GetEntitiesCount() > 0)
			{
				var upgradeEventComponent = world.GetPool<UpgradeEventComponent>().GetRawDenseItems()[1];
				ref var baseComponent = ref world.GetPool<BaseComponent>().GetRawDenseItems()[1];
				var configs = _configs.Value;

				switch (upgradeEventComponent.UpgradeType)
				{
					case UpgradeType.Damage when baseComponent.AttackDamageLevel < configs.MaxUpgradesLevel:

						Upgrade(world, baseComponent.AttackDamageLevel, (ref BaseComponent component) => 
						{
							component.AttackDamage++;
							component.AttackDamageLevel++;
							component.OnDamageUpgraded?.Invoke(component.AttackDamageLevel);
						});

						break;
					case UpgradeType.AttackSpeed when baseComponent.AttackSpeedLevel < configs.MaxUpgradesLevel:
						
						Upgrade(world, baseComponent.AttackSpeedLevel, (ref BaseComponent component) => 
						{
							component.AttackSpeed -= configs.AttackSpeedUpgradeValue;
							component.AttackSpeedLevel++;
							component.OnAttackSpeedUpgraded?.Invoke(component.AttackSpeedLevel);
						});

						break;
					case UpgradeType.Range when baseComponent.AttackRangeLevel < configs.MaxUpgradesLevel:
						
						Upgrade(world, baseComponent.AttackRangeLevel, (ref BaseComponent component) => 
						{
							component.AttackRange += configs.AttackRangeUpgradeValue;
							component.AttackRangeLevel++;
							component.OnRangeUpgraded?.Invoke(component.AttackRangeLevel);
							component.OnRangeChanged?.Invoke(component.AttackRange);
						});
						
						break;
				}
			}
		}

		private void Upgrade(EcsWorld world, int upgradeLevel, ActionRef<BaseComponent> action)
		{
			ref var baseComponent = ref world.GetPool<BaseComponent>().GetRawDenseItems()[1];
			var cost = upgradeLevel * _configs.Value.MaxUpgradesLevel;

			if (cost <= baseComponent.Money)
			{
				action?.Invoke(ref baseComponent);
				baseComponent.Money -= cost;
				baseComponent.OnMoneyValueChanged?.Invoke(baseComponent.Money);
			}
		}
	}
}