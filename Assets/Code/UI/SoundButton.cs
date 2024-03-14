using Code.Services.AudioService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Toggle _toggle;
        
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void TurnOn()
        {
            _toggle.onValueChanged.AddListener(OnToggleClick);

            _toggle.isOn = _audioService.IsMute;
        }

        public void TurnOff()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleClick);
        }

        private void OnToggleClick(bool isActive)
        {
            if(isActive == false)
            {
                _background.enabled = true;
                _audioService.Enable();
            }
            else
            {
                _background.enabled = false;
                _audioService.Disable();
            }
        }
    }
}
