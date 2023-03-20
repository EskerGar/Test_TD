using UnityEngine;

namespace DefaultNamespace
{
	public class ViewsKeeper : MonoBehaviour
	{
		[SerializeField] private BaseView _baseView;
		[SerializeField] private BulletView _bulletViewPrefab;
		[SerializeField] private EnemyView _enemyViewPrefab;
		[SerializeField] private HudUiView _hudUiView;

		public BaseView BaseView => _baseView;
		public BulletView BulletViewPrefab => _bulletViewPrefab;
		public EnemyView EnemyViewPrefab => _enemyViewPrefab;
		public HudUiView HUDUiView => _hudUiView;
	}
}