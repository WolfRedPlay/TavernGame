
using UnityEngine;

namespace Gameplay.Clients
{
    public class MovingOutState : ClientState
    {
        private Transform _movingTarget;

        public MovingOutState(ClientsStateManager stateManager, Transform movingTarget) : base(stateManager)
        {
            _movingTarget = movingTarget;
        }

        public override void OnStart()
        {
            _stateManager.MovementController.OnMovementFinished += OnMovementFinished;
            _stateManager.MovementController.MoveToTarget(_movingTarget);
        }

        private void OnMovementFinished()
        {
            _stateManager.MovementController.OnMovementFinished -= OnMovementFinished;
            _stateManager.DespawnClient();
        }

        public override void OnStop()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}
