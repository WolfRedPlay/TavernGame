using Core.Events;
using Core.Shared;
using UnityEngine.AI;

namespace Gameplay.Player 
{
    public class PlayerMovementController : MovementController
    {
        public override void Initialize(NavMeshAgent agent)
        {
            base.Initialize(agent);
            EventManager.AddListener<MovePlayerEvent>(OnPlayerMoveEvent);
        }


        private void OnPlayerMoveEvent(MovePlayerEvent evt)
        {
            MoveToTarget(evt.Target);
            _rotateToTarget = evt.RotateAtTheEnd;
        }


        protected override void FinishMovement()
        {
            EventManager.Broadcast(new PathFinishedEvent());

            base.FinishMovement();
        }


        private void OnDestroy()
        {
            EventManager.RemoveListener<MovePlayerEvent>(OnPlayerMoveEvent);
        }
    }
}