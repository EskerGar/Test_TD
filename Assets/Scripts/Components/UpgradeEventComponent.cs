namespace Components
{
	public enum UpgradeType
	{
		Damage,
		AttackSpeed,
		Range
	}
	
	public struct UpgradeEventComponent
	{
		public UpgradeType UpgradeType;
	}
}