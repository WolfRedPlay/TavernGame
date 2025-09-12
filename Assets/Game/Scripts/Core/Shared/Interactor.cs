using System;

namespace Core.Shared
{
    public abstract class Interactor
    {
        protected IInteractable _interactable;
        

        public MovementController MovementController { get; private set; }


        public Interactor(MovementController movementController)
        {
            MovementController = movementController;

            MovementController.OnMovementFinished += Interact;
        }


        private void Interact()
        {
            if (_interactable == null) return;

            _interactable.Interact(this);
            _interactable = null;
        }
    }
}
