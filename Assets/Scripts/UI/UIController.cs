using System;

namespace UI
{
	public class UIController
	{
		public event Action<bool> OnLockStatusChanged;
        
        private static UIController _instance;
        public static UIController Instance => _instance ??= new UIController();

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