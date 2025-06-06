using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopupView : MonoBehaviour
    {
        public event Action OnClick;
        
        [SerializeField] private TextMeshProUGUI _popupText;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void SetPopupText(string text)
        {
            _popupText.text = text;
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}