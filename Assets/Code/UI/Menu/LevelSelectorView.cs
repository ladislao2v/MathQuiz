using System.Collections;
using Code.Services.LevelSelectorService;
using Code.StateMachine;
using Code.StateMachine.States;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Menu
{
    public class LevelSelectorView : MonoBehaviour
    {
        [SerializeField] private LevelButton[] _levelButtons; 
        [SerializeField] private GameObject[] _crosses;
        
        private ILevelSelector _levelSelector;
        private IStateMachine _stateMachine;

        [Inject]
        public void Construct(ILevelSelector levelSelector, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _levelSelector = levelSelector;
        }

        public void SetCurrentLevel(int level, bool isOpen)
        {
            if(isOpen)
                MakeAvailable(level);
            else
                MakeNotAvailable(level);
        }

        private void MakeAvailable(int level)
        {
            _crosses[level-1].SetActive(false);
            _levelButtons[level - 1].Enable();
        }

        private void MakeNotAvailable(int level)
        {
            _crosses[level-1].SetActive(true);
            _levelButtons[level - 1].Disable();
        }

        public void TurnOn()
        {
            foreach (var levelButton in _levelButtons)
            {
                levelButton.Clicked += OnLevelButtonClicked;
                levelButton.TurnOn();
            }
        }

        private void OnLevelButtonClicked(int index)
        {
            _levelSelector.Select(index);
            _stateMachine.Enter<QuestionsState>();
        }

        public void TurnOff()
        {
            foreach (var levelButton in _levelButtons)
                levelButton.TurnOff();
        }
    }
}