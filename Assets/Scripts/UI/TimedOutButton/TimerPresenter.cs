using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class TimerPresenter : MonoBehaviour
	{
		[SerializeField] private TMP_Text text;

		private Timer _timer;

		public void Init(Timer timer)
		{
			_timer = timer;
			_timer.OnTimerUpdate += TimerUpdate;
		}

		private void OnDestroy()
		{
			_timer.OnTimerUpdate -= TimerUpdate;
		}

		private void TimerUpdate(int timeLeft)
		{
			text.text = timeLeft.ToString();
		}
	}
}