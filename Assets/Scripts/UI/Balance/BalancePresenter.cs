using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using User.Balance;

namespace UI.Balance
{
	public class BalancePresenter : MonoBehaviour
	{
		[SerializeField] private TMP_Text amount;

		private int _currentAmount;
		private int _desiredAmount;
		private IBalance _balance;

		private bool _taskRunning;

		public void Init(IBalance balance)
		{
			_balance = balance;
			_balance.OnBalanceChanged += BalanceChanged;
			BalanceChanged(_balance.Get());
		}

		private void OnDestroy()
		{
			_balance.OnBalanceChanged -= BalanceChanged;
		}

		private void BalanceChanged(int value)
		{
			_desiredAmount = value;
			if (!_taskRunning)
			{
				ChangeBalance();
			}
		}

		private async void ChangeBalance()
		{
			_taskRunning = true;
			var lastDesiredAmount = _desiredAmount;
			var delayDelta = _desiredAmount == _currentAmount ? 10 :  1000 / (_desiredAmount - _currentAmount);
			do
			{
				if (lastDesiredAmount != _desiredAmount)
				{
					lastDesiredAmount = _desiredAmount;
					delayDelta = _desiredAmount == _currentAmount ? 10 :  1000 / (_desiredAmount - _currentAmount);
				}
				
				amount.text = _currentAmount.ToString();
				await Task.Delay(delayDelta);
			} while (_currentAmount++ < _desiredAmount);
			_taskRunning = false;
		}
	}
}