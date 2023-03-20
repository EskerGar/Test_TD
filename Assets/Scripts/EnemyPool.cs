using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class EnemyPool
	{
		private readonly EnemyView _prefab;
		private readonly Stack<EnemyView> _pool = new();

		public EnemyPool(EnemyView prefab)
		{
			_prefab = prefab;
		}

		public EnemyView Get()
		{
			if (_pool.Count > 0)
			{
				var view = _pool.Pop();
				view.gameObject.SetActive(true);
				
				return view;
			}
			else
			{
				return Object.Instantiate(_prefab);
			}
		}

		public void Put(EnemyView view)
		{
			_pool.Push(view);
			view.gameObject.SetActive(false);
		}
	}
}