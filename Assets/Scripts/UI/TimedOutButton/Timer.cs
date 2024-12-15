using System;
using System.Threading.Tasks;
using UI.Interface;

namespace UI
{
	public class Timer : IModel
	{
		public event Action<int> OnTimerUpdate;
		public event Action OnTimerExpired;

		private bool _timerAwake;
		private int _timerDuration;
		
		public void Dispose()
		{
			_timerAwake = false;
		}

		public void StartTimer(int duration)
		{
			_timerAwake = true;
			_timerDuration = duration;
			UpdateTimer();
		}

		private async void UpdateTimer()
		{
			do
			{
				OnTimerUpdate?.Invoke(_timerDuration);
				await Task.Delay(1000);
			} while (_timerAwake && _timerDuration-- > 0);
			
			OnTimerExpired?.Invoke();
		}

		public void StopTimer()
		{
			_timerAwake = false;
		}
	}
}