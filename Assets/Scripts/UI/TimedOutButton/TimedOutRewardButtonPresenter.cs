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
        
        public bool Enabled
        {
            set
            {
                button.interactable = value;
                
                titleGo.SetActive(value);
                timerGo.SetActive(!value);
            }
        }
        
        public bool Locked
        {
            set => button.interactable = !value;
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
