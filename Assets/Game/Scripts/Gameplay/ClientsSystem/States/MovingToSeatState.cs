using Gameplay.Tables;

namespace Gameplay.Clients
{
    public class MovingToSeatState : ClientState
    {
        private Seat _assignedSeat;

        public MovingToSeatState(ClientsStateManager stateManager, Seat assignedSeat) : base(stateManager)
        {
            _assignedSeat = assignedSeat;
        }

        public override void OnStart()
        {
            _stateManager.MovementController.SetAgentPriority(3);
            _stateManager.MovementController.OnMovementFinished += OnMovementFinished;
            _stateManager.MovementController.MoveToTarget(_assignedSeat.ApproachPoint);
            _stateManager.Animator.SetWalking(true);
        }

        public override void OnStop()
        {
            _stateManager.MovementController.OnMovementFinished -= OnMovementFinished;
            _stateManager.Animator.SetWalking(false);
        }

        public override void OnUpdate()
        {
        }

        public void OnMovementFinished()
        {
            _stateManager.SetState<SeatingState>();
        }
    }
}