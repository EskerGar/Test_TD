using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class BaseView : MonoBehaviour
	{
		[SerializeField] private Image _attackRange;

		public void SetAttackRange(float attackRange)
		{
			_attackRange.transform.localScale = new Vector3(attackRange, attackRange, attackRange);
		}
	}
}