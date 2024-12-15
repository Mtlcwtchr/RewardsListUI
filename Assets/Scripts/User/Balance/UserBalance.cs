using System;
using UnityEngine;

namespace User.Balance
{
	public class UserBalance : IBalance
	{
		private const string BalanceKey = "Balance";
		
		public event Action<int> OnBalanceChanged;

		private int _balance;

		public UserBalance()
		{
			_balance = PlayerPrefs.GetInt(BalanceKey);
		}
		
		public int Get()
		{
			return _balance;
		}

		public void Advance(int amount, IBalanceAdvanceSource advanceSource = null)
		{
			_balance += amount;
			PlayerPrefs.SetInt(BalanceKey, _balance);
			OnBalanceChanged?.Invoke(_balance);
			advanceSource?.NotifyConsumed();
		}
	}
}