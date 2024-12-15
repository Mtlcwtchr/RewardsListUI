using System;
using System.Collections.Generic;
using Rewards;
using UI.Interface;
using User.Balance;

namespace UI.Rewards
{
	public class RewardsPanel : IModel
	{
		public event Action OnClose;
		public event Action OnRewardsFetched;
		
		private RewardsPanelPresenter _presenter;
		private IRewardsRepository _repository;
		private IBalance _balance;

		private List<RewardData> _rewards;
		private List<RewardElement.RewardElement> _rewardElements;

		public bool Visible
		{
			set
			{
				if (value)
				{
					_presenter.Show();
				}
				else
				{
					_presenter.Hide();
				}
			}
		}

		public RewardsPanel(RewardsPanelPresenter presenter, IRewardsRepository repository, IBalance balance)
		{
			_presenter = presenter;
			_repository = repository;
			_balance = balance;
			_rewardElements = new List<RewardElement.RewardElement>();
			
			_presenter.OnCloseClick += CloseClick;
		}

		public async void FetchRewards()
		{
			_rewards = await _repository.ReadAllRewards();
			for (var i = 0; i < _rewards.Count; i++)
			{
				BuildElement(_rewards[i]);
			}
			OnRewardsFetched?.Invoke();
		}
		
		public void Dispose()
		{
			_presenter.OnCloseClick -= CloseClick;
		}

		public void BuildElement(RewardData data)
		{
			var newElement = _presenter.CreateReward();
			
			var model = new RewardElement.RewardElement(newElement, data, this, _repository, _balance);
			_rewardElements.Add(model);
			newElement.Init(model);
		}

		private void CloseClick()
		{
			OnClose?.Invoke();
		}
		
	}
}