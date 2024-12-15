using UnityEngine;

namespace UI
{
	[CreateAssetMenu(menuName = "UI/TimedOutRewardButton", fileName = "TimedOutRewardButtonSettings")]
	public class TimedOutRewardButtonSettings : ScriptableObject
	{
		public int rewardAmount;
		public int timerDuration;
	}
}