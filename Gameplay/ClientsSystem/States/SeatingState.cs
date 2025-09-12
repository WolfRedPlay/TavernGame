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

            _stateManager.MovementController.transform.position = _assignedSeat.SeatPoint.position;
            _stateManager.MovementController.transform.rotation = _assignedSeat.SeatPoint.rotation;

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
