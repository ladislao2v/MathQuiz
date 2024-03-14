using Code.UI.Discription;

namespace Code.StateMachine.States
{
    public class PolicyState : IState
    {
        private readonly PolicyOverlay _policyOverlay;

        public PolicyState(PolicyOverlay policyOverlay)
        {
            _policyOverlay = policyOverlay;
        }
        
        public void Enter()
        {
            _policyOverlay.Show();
        }

        public void Exit()
        {
            _policyOverlay.Hide();
        }
    }
}