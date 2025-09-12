using Core.Events;
using Core.Shared;
using UnityEngine.AI;

namespace Gameplay.Player 
{
    public class PlayerMovementController : MovementController
    {
        IInteractable _interactable;

        public override void Initialize(NavMeshAgent agent)
        {
            base.Initialize(agent);
            EventManager.AddListener<MovePlayerEvent>(OnPlayerMoveEvent);
        }


        private void OnPlayerMoveEvent(MovePlayerEvent evt)
        {
            MoveToTarget(evt.Target);
            _interactable = evt.Interactable;
        }


        protected override void FinishMovement()
        {
            EventManager.Broadcast(new PathFinishedEvent());

            if (_interactable != null)
            {
                _interactable.Interact();
                _interactable = null;
            }

            base.FinishMovement();
        }


        private void OnDestroy()
        {
            EventManager.RemoveListener<MovePlayerEvent>(OnPlayerMoveEvent);
        }
    }
}