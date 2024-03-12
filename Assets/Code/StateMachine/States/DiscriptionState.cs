using Code.UI.Discription;

namespace Code.StateMachine.States
{
    public class DiscriptionState : IState
    {
        private readonly DiscriptionOverlay _discriptionOverlay;

        public DiscriptionState(DiscriptionOverlay discriptionOverlay)
        {
            _discriptionOverlay = discriptionOverlay;
        }
        
        public void Enter()
        {
            _discriptionOverlay.Show();
        }

        public void Exit()
        {
            _discriptionOverlay.Hide();
        }
    }
}