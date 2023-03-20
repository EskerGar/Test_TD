using System;
using DefaultNamespace;

namespace Components
{
	public struct BaseComponent
	{
		public BaseView BaseView;
		public int AttackDamage;
		public float AttackRange;
		public float AttackSpeed;
		public int AttackDamageLevel;
		public int AttackRangeLevel;
		public int AttackSpeedLevel;
		public int Money;
		public Action<int> OnMoneyValueChanged;
		public Action<int> OnDamageUpgraded;
		public Action<int> OnAttackSpeedUpgraded;
		public Action<int> OnRangeUpgraded;
		public Action<float> OnRangeChanged;
	}
}