using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class LevelSelectorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _cross;
        public Button LeftButton => _leftButton;
        public Button RightButton => _rightButton;

        public void SetCurrentLevel(int level, bool isOpen)
        {
            _levelText.text = level + " Level";
            
            if(isOpen)
                MakeAvailable();
            else
                MakeNotAvailable();
        }

        private void MakeAvailable()
        {
            _cross.SetActive(false);
            _playButton.SetActive(true);
        }

        private void MakeNotAvailable()
        {
            _cross.SetActive(true);
            _playButton.SetActive(false);
        }
    }
}