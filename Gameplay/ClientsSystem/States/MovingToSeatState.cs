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
            _stateManager.MovementController.OnMovementFinished += OnMovementFinished;
            _stateManager.MovementController.MoveToTarget(_assignedSeat.ApproachPoint);
        }

        public override void OnStop()
        {
            _stateManager.MovementController.OnMovementFinished -= OnMovementFinished;
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