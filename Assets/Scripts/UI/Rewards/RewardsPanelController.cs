using UnityEngine;
using UnityEngine.UI;

namespace UI.Rewards
{
	public class RewardsPanelController : MonoBehaviour
	{
		[SerializeField] private Button showRewardsButton;

		private RewardsPanel _rewardsPanel;
		
		public void Init(RewardsPanel rewardsPanel)
		{
			_rewardsPanel = rewardsPanel;
			_rewardsPanel.OnRewardsFetched += RewardsFetched;
			_rewardsPanel.OnClose += CloseRewards;
			UIController.Instance.OnLockStatusChanged += OnLock;
		}

		private void RewardsFetched()
		{
			showRewardsButton.interactable = true;
		}

		private void Awake()
		{
			showRewardsButton.interactable = false;
			showRewardsButton.onClick.AddListener(ShowRewards);
		}

		private void OnDestroy()
		{
			_rewardsPanel.OnRewardsFetched -= RewardsFetched;
			_rewardsPanel.OnClose -= CloseRewards;
			UIController.Instance.OnLockStatusChanged -= OnLock;
			showRewardsButton.onClick.RemoveListener(ShowRewards);
		}

		private void ShowRewards()
		{
			_rewardsPanel.Visible = true;
		}

		private void OnLock(bool value)
		{
			showRewardsButton.interactable = !value;
		}

		private void CloseRewards()
		{
			_rewardsPanel.Visible = false;
		}
	}
}