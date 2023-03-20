using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class BulletPool
	{
		private readonly BulletView _prefab;
		private readonly Stack<BulletView> _pool = new();

		public BulletPool(BulletView prefab)
		{
			_prefab = prefab;
		}

		public BulletView Get()
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

		public void Put(BulletView view)
		{
			_pool.Push(view);
			view.gameObject.SetActive(false);
		}
	}
}