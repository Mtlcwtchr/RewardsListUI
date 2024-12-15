using System;

namespace User.Balance
{
	public interface IBalance
	{
		public event Action<int> OnBalanceChanged; 
		
		public int Get();
		public void Advance(int amount, IBalanceAdvanceSource advanceSource = null);
	}
}