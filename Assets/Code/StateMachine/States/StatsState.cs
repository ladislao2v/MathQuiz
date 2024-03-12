using Code.UI.Stats;

namespace Code.StateMachine.States
{
    public class StatsState : IState
    {
        private readonly StatsOverlay _stats;

        public StatsState(StatsOverlay stats)
        {
            _stats = stats;
        }
        
        public void Enter()
        {
            _stats.Show();
        }

        public void Exit()
        {
            _stats.Hide();
        }
    }
}