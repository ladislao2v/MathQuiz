using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class VariantButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _variantText;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _truePanel;
        [SerializeField] private GameObject _falsePanel;

        private bool _isAnswer;

        public event Action<string> Clicked; 

        public void Construct(string variant, bool isAnswer)
        {
            _isAnswer = isAnswer;
            _variantText.text = variant;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Reset()
        {
            _button.enabled = true;
            _truePanel.SetActive(false);
            _falsePanel.SetActive(false);
        }

        public void Disable()
        {
            _button.enabled = false;
        }

        public void Subscribe(Action<string> action)
        {
            Clicked += action;
        }

        public void Unsubscribe(Action<string> action)
        {
            Clicked -= action;
        }

        private void OnButtonClicked()
        {
            if(_isAnswer)
                _truePanel.SetActive(true);
            else
                _falsePanel.SetActive(true);

            Clicked?.Invoke(_variantText.text);
        }
    }
}