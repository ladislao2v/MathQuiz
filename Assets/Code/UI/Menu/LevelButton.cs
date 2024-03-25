using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _index;

        public event Action<int> Clicked;
        
        public void TurnOn()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void TurnOff()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke(_index);
        }

        public void Enable()
        {
            _button.interactable = true;
        }

        public void Disable()
        {
            _button.interactable = false;
        }
    }
}