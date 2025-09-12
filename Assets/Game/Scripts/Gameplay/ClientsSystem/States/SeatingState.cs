using Gameplay.Tables;

namespace Gameplay.Clients
{
    public class SeatingState : ClientState
    {
        Seat _assignedSeat;

        public SeatingState(ClientsStateManager stateManager, Seat assignedSeat) : base(stateManager)
        {
            _assignedSeat = assignedSeat;
        }

        public override void OnStart()
        {
            _stateManager.MovementController.SetAgentActive(false);

            _stateManager.Animator.StartSitDownAnimation();

            _stateManager.Animator.OnSitDown += OnSitDown;
        }

        private void OnSitDown()
        {
            _stateManager.Animator.OnSitDown -= OnSitDown;

            _stateManager.MovementController.transform.position = _assignedSeat.transform.position;
            _stateManager.MovementController.transform.forward = _assignedSeat.transform.right;

            _stateManager.SetState<WaitingState>();
        }

        public override void OnStop()
        {
            _stateManager.TrigerClientSatDown();
        }

        public override void OnUpdate()
        {
        }
    }
}
