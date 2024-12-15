using System;
using Core;

namespace UI
{
	public class UIController : MonoBehaviourSingleton<UIController>
	{
		public event Action<bool> OnLockStatusChanged;

		private bool _locked;
		public bool Locked
		{
			get => _locked;
			set
			{
				if (_locked == value) return;
				
				_locked = value;
				OnLockStatusChanged?.Invoke(_locked);
			}
		}
	}
}