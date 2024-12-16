using System;
using Rewards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RewardElement
{
	public class RewardElementPresenter : MonoBehaviour
	{
		public event Action OnClaimClick;
		
		[SerializeField] private Image avatar;
		[SerializeField] private GameObject avatarGo;
		[SerializeField] private GameObject loaderGo;
		[SerializeField] private TMP_Text title;
		[SerializeField] private TMP_Text message;
		[SerializeField] private TMP_Text amount;
		[SerializeField] private Button claimButton;
		[SerializeField] private GameObject buttonGo;
		[SerializeField] private GameObject claimedGo;

		private RewardElement _model;

		private Sprite _avatar;

		private bool _enabled = true;
		public bool Enabled
		{
			get => _enabled;
			set
			{
				_enabled = value;
				claimButton.interactable = _enabled;
				buttonGo.SetActive(_enabled);
				claimedGo.SetActive(!_enabled);
			}
		}

		public bool Locked
		{
			set => claimButton.interactable = !value && _enabled;
		}
		
		private void Awake()
		{
			claimButton.onClick.AddListener(ClaimClick);
		}

		private void OnDestroy()
		{
			claimButton.onClick.RemoveListener(ClaimClick);
		}

		public void Init(RewardElement model)
		{
			_model = model;
			_model.UpdateData();
		}

		public void UpdateData(RewardData data)
		{
			title.text = data.title;
			message.text = data.message;
			amount.text = data.amount.ToString();
			Enabled = !data.claimed;
			LoadAvatar(data);
		}

		private async void LoadAvatar(RewardData data)
		{
			if (_avatar)
			{
				avatar.sprite = _avatar;
				return;
			}
			
			loaderGo.SetActive(true);
			avatarGo.SetActive(false);
			
			_avatar = await _model.LoadAvatar(data.avatarLink);
			if(_avatar != null)
				avatar.sprite = _avatar;
			
			loaderGo.SetActive(false);
			avatarGo.SetActive(true);
		}

		public void PlayRewardAnimation()
		{
            
		}

		private void ClaimClick()
		{
			OnClaimClick?.Invoke();
		}
		
	}
}