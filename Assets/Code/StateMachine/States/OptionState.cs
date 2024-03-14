using Code.UI.Options;

namespace Code.StateMachine.States
{
    public class OptionState : IState
    {
        private readonly OptionsOverlay _optionsOverlay;

        public OptionState(OptionsOverlay optionsOverlay)
        {
            _optionsOverlay = optionsOverlay;
        }
        
        public void Enter()
        {
            _optionsOverlay.Show();
        }

        public void Exit()
        {
            _optionsOverlay.Hide();
        }
    }
}