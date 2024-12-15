using System;
using DG.Tweening;
using UI.RewardElement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Rewards
{
	public class RewardsPanelPresenter : MonoBehaviour
	{
		public event Action OnCloseClick;
		
		[SerializeField] private RewardElementPresenter template;
		[SerializeField] private Transform root;

		[SerializeField] private Button closeButton;
		
		private RewardsPanel _model;

		private void Awake()
		{
			closeButton.onClick.AddListener(CloseClick);
		}

		private void OnDestroy()
		{
			closeButton.onClick.RemoveListener(CloseClick);
		}

		public void Init(RewardsPanel model)
		{
			_model = model;
		}
		
		public RewardElementPresenter CreateReward()
		{
			var newElement = Instantiate(template, root);
			return newElement;
		}

		public void Show()
		{
			gameObject.SetActive(true);

			transform.localScale = Vector3.zero;
			UIController.Instance.Locked = true;
			transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => UIController.Instance.Locked = false);
		}


		public void Hide()
		{
			UIController.Instance.Locked = true;
			transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
			{
				gameObject.SetActive(false);
				UIController.Instance.Locked = false;
			});
		}

		private void CloseClick()
		{
			OnCloseClick?.Invoke();
		}
	}
}