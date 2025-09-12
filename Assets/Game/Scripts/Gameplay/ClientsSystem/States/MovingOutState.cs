
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
            _stateManager.MovementController.SetAgentPriority(2);
            _stateManager.Animator.SetWalking(true);
            _stateManager.MovementController.OnMovementFinished += OnMovementFinished;
            _stateManager.MovementController.MoveToTarget(_movingTarget);
        }

        private void OnMovementFinished()
        {
            _stateManager.MovementController.OnMovementFinished -= OnMovementFinished;
            _stateManager.Animator.SetWalking(false);
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
