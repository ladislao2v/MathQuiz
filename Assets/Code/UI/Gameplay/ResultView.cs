using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Gameplay
{
    public class ResultView : Overlay
    {
        [SerializeField] private Image _win;
        [SerializeField] private Image _lose;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Button _button;
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Construct(bool isWin)
        {
            if (isWin)
                _win.enabled = true;
            else
                _lose.enabled = true;
        }

        private void OnEnable()
        {
            _scoreView.TurnOn();
            _button.onClick.AddListener(OnMenu);
        }

        private void OnDisable()
        {
            _scoreView.TurnOff();
            _button.onClick.RemoveListener(OnMenu);
        }

        private void OnMenu()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}