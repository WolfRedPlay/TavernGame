
using Gameplay.Tables;

namespace Gameplay.Clients
{
    public class StandingUpState : ClientState
    {
        Seat _assignedSeat;

        public StandingUpState(ClientsStateManager stateManager, Seat assignedSeat) : base(stateManager)
        {
            _assignedSeat = assignedSeat;
        }

        public override void OnStart()
        {
            _stateManager.MovementController.transform.position = _assignedSeat.ApproachPoint.position;
            _stateManager.MovementController.transform.rotation = _assignedSeat.ApproachPoint.rotation;

            _stateManager.MovementController.SetAgentActive(true);
            _stateManager.SetState<MovingOutState>();
        }

        public override void OnStop()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}
