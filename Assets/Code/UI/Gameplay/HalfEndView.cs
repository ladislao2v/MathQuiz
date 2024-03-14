using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class HalfEndView : Overlay
    {
        [SerializeField] private ScoreView _scoreView;
        [field: SerializeField] public Button NextHalf { get; private set; }

        private void OnEnable()
        {
            _scoreView.TurnOn();
        }

        private void OnDisable()
        {
            _scoreView.TurnOff();
        }
    }
}