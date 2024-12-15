using System.Threading.Tasks;
using Rewards;
using UI.Interface;
using UI.Rewards;
using UnityEngine;
using UnityEngine.Networking;
using User.Balance;

namespace UI.RewardElement
{
	public class RewardElement : IModel, IBalanceAdvanceSource
	{
		private RewardElementPresenter _presenter;
		private IBalance _balance;

		private RewardData _data;
		private IRewardsRepository _rewardsRepository;

		private RewardsPanel _panel;

		public bool Locked
		{
			set => _presenter.Locked = value;
		}
		
		public RewardElement(RewardElementPresenter presenter, RewardData data, RewardsPanel panel, IRewardsRepository rewardsRepository, IBalance balance)
		{
			_presenter = presenter;
			_balance = balance;
			_data = data;
			_rewardsRepository = rewardsRepository;
			_panel = panel;
			
			_presenter.OnClaimClick += ClaimClick;
			UIController.Instance.OnLockStatusChanged += OnLock;
		}

		public void Dispose()
		{
			_presenter.OnClaimClick -= ClaimClick;
			UIController.Instance.OnLockStatusChanged -= OnLock;
		}

		public void UpdateData()
		{
			_presenter.UpdateData(_data);
		}

		private async void ClaimClick()
		{
			_balance.Advance(_data.amount, this);
			_data.claimed = true;
			_presenter.Enabled = false;

			UIController.Instance.Locked = true;
			await _rewardsRepository.UpdateData(_data);
			UIController.Instance.Locked = false;
		}

		public void NotifyConsumed()
		{
			_presenter.PlayRewardAnimation();
		}

		private void OnLock(bool value)
		{
			Locked = value;
		}

		public async Task<Sprite> LoadAvatar(string uri)
		{
			var request = UnityWebRequestTexture.GetTexture(uri);

			var op = request.SendWebRequest();

			while (!op.isDone)
				await Task.Delay(100);

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError($"Could not load avatar from uri {uri} : {request.error}");

				return null;
			}
			
			var tex = DownloadHandlerTexture.GetContent(request);
			var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
			return sprite;
		}
	}
}