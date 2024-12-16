using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimedOutRewardButtonPresenter : MonoBehaviour
    {
        public event Action OnClaimClick;
        
        [SerializeField] private GameObject timerGo;
        [SerializeField] private GameObject titleGo;
        [SerializeField] private Button button;

        private TimedOutRewardButton _model;

        private bool _enabled = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                button.interactable = _enabled;

                titleGo.SetActive(_enabled);
                timerGo.SetActive(!_enabled);
            }
        }
        
        public bool Locked
        {
            set => button.interactable = !value && Enabled;
        }

        private void Awake()
        {
            button.onClick.AddListener(Claim);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Claim);
        }

        public void Init(TimedOutRewardButton model)
        {
            _model = model;
        }

        public void PlayRewardAnimation()
        {
            
        }

        private void Claim()
        {
            OnClaimClick?.Invoke();
        }

    }
}
