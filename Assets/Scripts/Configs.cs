using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "Configs", menuName = "Configs/Configs")]
	public class Configs : ScriptableObject
	{
		[SerializeField] private float _enemyMoveSpeed;
		[SerializeField] private int _initialUpgradeCost;
		[SerializeField] private int _killReward;
		[SerializeField] private float _waveTime;
		[SerializeField] private float _initialAttackRange;
		[SerializeField] private float _initialAttackSpeed;
		[SerializeField] private int _maxUpgradesLevel;
		[SerializeField] private int _enemyHealth;
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private float _maxSpawnRange;
		[SerializeField] private float _attackSpeedUpgradeValue;
		[SerializeField] private float _attackRangeUpgradeValue;

		public float EnemyMoveSpeed => _enemyMoveSpeed;
		public int InitialUpgradeCost => _initialUpgradeCost;
		public int KillReward => _killReward;
		public float WaveTime => _waveTime;
		public float InitialAttackRange => _initialAttackRange;
		public float InitialAttackSpeed => _initialAttackSpeed;
		public int MaxUpgradesLevel => _maxUpgradesLevel;
		public int EnemyHealth => _enemyHealth;
		public float BulletSpeed => _bulletSpeed;
		public float MaxSpawnRange => _maxSpawnRange;
		public float AttackSpeedUpgradeValue => _attackSpeedUpgradeValue;
		public float AttackRangeUpgradeValue => _attackRangeUpgradeValue;
	}
}