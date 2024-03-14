
using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class PauseView : Overlay
    {
        [SerializeField] private ScoreView _scoreView;
        [field: SerializeField] public Button RestartButton;
        [field: SerializeField] public Button ContinueButton;
        [field: SerializeField] public Button ExitButton;

        public void TurnOn()
        {
            _scoreView.TurnOn();
        }

        public void TurnOff()
        {
            _scoreView.TurnOff();
        }
    }
}