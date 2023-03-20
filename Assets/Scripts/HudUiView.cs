using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class HudUiView : MonoBehaviour
	{
		[SerializeField] private Configs _configs;
		[SerializeField] private Button _upgradeDamageButton;
		[SerializeField] private Button _upgradeAttackSpeedButton;
		[SerializeField] private Button _upgradeRangeButton;
		[SerializeField] private TMP_Text _money;
		[SerializeField] private TMP_Text _damageUpgrade;
		[SerializeField] private TMP_Text _attackSpeedUpgrade;
		[SerializeField] private TMP_Text _rangeUpgrade;

		public void SetOnUpgradeDamageButtonClick(UnityAction action)
		{
			_upgradeDamageButton.onClick.AddListener(action);
		}
		
		public void SetOnUpgradeAttackSpeedButtonClick(UnityAction action)
		{
			_upgradeAttackSpeedButton.onClick.AddListener(action);
		}
		
		public void SetOnUpgradeRangeButtonClick(UnityAction action)
		{
			_upgradeRangeButton.onClick.AddListener(action);
		}

		public void SetDamageUpgrade(int levelUpgrade)
		{
			var nextLevelCost = levelUpgrade < _configs.MaxUpgradesLevel ? (levelUpgrade * _configs.InitialUpgradeCost).ToString() : "max level";
			_damageUpgrade.SetText($"damage lvl {levelUpgrade.ToString()} next level cost {nextLevelCost}");
		}
		
		public void SetAttackSpeedUpgrade(int levelUpgrade)
		{
			var nextLevelCost = levelUpgrade < _configs.MaxUpgradesLevel ? (levelUpgrade * _configs.InitialUpgradeCost).ToString() : "max level";
			_attackSpeedUpgrade.SetText($"attackSpeed lvl {levelUpgrade.ToString()} next level cost {nextLevelCost}");
		}
		
		public void SetRangeUpgrade(int levelUpgrade)
		{
			var nextLevelCost = levelUpgrade < _configs.MaxUpgradesLevel ? (levelUpgrade * _configs.InitialUpgradeCost).ToString() : "max level";
			_rangeUpgrade.SetText($"range lvl {levelUpgrade.ToString()} next level cost {nextLevelCost}");
		}

		public void SetMoney(int money)
		{
			_money.SetText($"Money {money.ToString()}");
		}
	}
}