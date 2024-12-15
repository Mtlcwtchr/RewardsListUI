using UI.Interface;
using User.Balance;

namespace UI
{
	public class TimedOutRewardButton : IModel, IBalanceAdvanceSource
	{
		private TimedOutRewardButtonPresenter _presenter;
		private Timer _timer;
		private TimedOutRewardButtonSettings _settings;
		private IBalance _balance;
		
		public TimedOutRewardButton(TimedOutRewardButtonPresenter presenter, Timer timer, TimedOutRewardButtonSettings settings, IBalance balance)
		{
			_presenter = presenter;
			_timer = timer;
			_settings = settings;
			_balance = balance;
			
			_presenter.OnClaimClick += ClaimClick;
			_timer.OnTimerExpired += TimerExpired;
			UIController.Instance.OnLockStatusChanged += OnLock;
		}

		public void Dispose()
		{
			_presenter.OnClaimClick -= ClaimClick;
			_timer.OnTimerExpired -= TimerExpired;
			UIController.Instance.OnLockStatusChanged -= OnLock;
		}

		private void ClaimClick()
		{
			_balance.Advance(_settings.rewardAmount, this);
			_presenter.Enabled = false;
			_timer.StartTimer(_settings.timerDuration);
		}

		private void TimerExpired()
		{
			_presenter.Enabled = true;
		}

		private void OnLock(bool value)
		{
			_presenter.Locked = value;
		}

		public void NotifyConsumed()
		{
			_presenter.PlayRewardAnimation();
		}
	}
}