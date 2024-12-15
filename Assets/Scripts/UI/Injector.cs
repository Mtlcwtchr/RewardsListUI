using Rewards;
using UI.Balance;
using UI.Rewards;
using UnityEngine;
using User.Balance;

namespace UI
{
	public class Injector : MonoBehaviour
	{
		[SerializeField] private TimedOutRewardButtonPresenter rewardButton;
		[SerializeField] private TimedOutRewardButtonSettings rewardButtonSettings;
		[SerializeField] private TimerPresenter rewardTimer;

		[SerializeField] private BalancePresenter balancePresenter;

		[SerializeField] private RewardsPanelPresenter rewardsPanel;

		[SerializeField] private RewardsPanelController rewardsPanelController;

		private IBalance _balance;
		
		protected void OnEnable()
		{
			_balance = new UserBalance();
			balancePresenter.Init(_balance);
				
			BuildDeps(_balance);

			var repo = new RewardsRepository($"{Application.dataPath}/reward_data.json");
			var rewardsPanelModel = new RewardsPanel(rewardsPanel, repo, _balance);
			rewardsPanel.Init(rewardsPanelModel);
			rewardsPanelController.Init(rewardsPanelModel);
			rewardsPanelModel.FetchRewards();
		}

		private void BuildDeps(IBalance balance)
		{
			var timer = new Timer();
			var rewardButtonModel = new TimedOutRewardButton(rewardButton, timer, rewardButtonSettings, balance);
			rewardButton.Init(rewardButtonModel);
			rewardTimer.Init(timer);
		}
	}
}