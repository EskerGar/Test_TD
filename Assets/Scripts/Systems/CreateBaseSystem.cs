using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class CreateBaseSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;
		
		public void Init(IEcsSystems systems)
		{
			var configs = _configs.Value;
			var world = systems.GetWorld();
			var basePool = world.GetPool<BaseComponent>();

			var baseEntity = world.NewEntity();
			basePool.Add(baseEntity);

			ref var baseComponent = ref basePool.Get(baseEntity);
			baseComponent.AttackDamage = 1;
			baseComponent.AttackDamageLevel = 1;
			baseComponent.AttackRange = configs.InitialAttackRange;
			baseComponent.AttackRangeLevel = 1;
			baseComponent.AttackSpeed = configs.InitialAttackSpeed;
			baseComponent.AttackSpeedLevel = 1;
			baseComponent.BaseView = _viewsKeeper.Value.BaseView;
			_viewsKeeper.Value.BaseView.SetAttackRange(configs.InitialAttackRange);

			baseComponent.OnRangeChanged += _viewsKeeper.Value.BaseView.SetAttackRange;
			
			var hudUiView = _viewsKeeper.Value.HUDUiView;
			hudUiView.SetOnUpgradeDamageButtonClick(() => Upgrade(world, baseEntity, UpgradeType.Damage));
			hudUiView.SetOnUpgradeAttackSpeedButtonClick(() => Upgrade(world, baseEntity, UpgradeType.AttackSpeed));
			hudUiView.SetOnUpgradeRangeButtonClick(() => Upgrade(world, baseEntity, UpgradeType.Range));

			baseComponent.OnMoneyValueChanged += hudUiView.SetMoney;
			baseComponent.OnDamageUpgraded += hudUiView.SetDamageUpgrade;
			baseComponent.OnAttackSpeedUpgraded += hudUiView.SetAttackSpeedUpgrade;
			baseComponent.OnRangeUpgraded += hudUiView.SetRangeUpgrade;
			
			hudUiView.SetMoney(baseComponent.Money);
			hudUiView.SetDamageUpgrade(baseComponent.AttackDamageLevel);
			hudUiView.SetAttackSpeedUpgrade(baseComponent.AttackSpeedLevel);
			hudUiView.SetRangeUpgrade(baseComponent.AttackRangeLevel);
		}

		private void Upgrade(EcsWorld world, int baseEntity, UpgradeType upgradeType)
		{
			var upgradeEventPool = world.GetPool<UpgradeEventComponent>();
			upgradeEventPool.Add(baseEntity);
			ref var upgradeEventComponent = ref upgradeEventPool.Get(baseEntity);
			upgradeEventComponent.UpgradeType = upgradeType;
		}
	}
}