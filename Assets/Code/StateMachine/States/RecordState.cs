using Code.UI.Stats;

namespace Code.StateMachine.States
{
    public class RecordState : IState
    {
        private readonly RecordOverlay _record;

        public RecordState(RecordOverlay @record)
        {
            _record = record;
        }
        
        public void Enter()
        {
            _record.Show();
        }

        public void Exit()
        {
            _record.Hide();
        }
    }
}