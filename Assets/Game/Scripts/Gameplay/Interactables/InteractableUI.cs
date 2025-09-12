
using Core.Shared;
using UnityEngine;

namespace Gameplay.Interactables
{
    public class InteractableUI<T>: MoveTarget, IInteractable where T : UI_Interactable
    {
        protected T _UI;
        protected bool _isOpen = false;
        protected Interactor _currentInteractor;


        public override void Initialize()
        {
            base.Initialize();

            _UI = FindAnyObjectByType<T>(FindObjectsInactive.Include);
            _UI.Initialize();
        }


        public virtual void Interact(Interactor interactor)
        {
            if (_isOpen) return;
            ShowUI();
            _isOpen = true;
            _currentInteractor = interactor;

            if (_currentInteractor != null) _currentInteractor.MovementController.SetBusy(true);
        }

        public virtual void StopInteraction()
        {
            _UI.OnCloseButtonClick -= StopInteraction;
            _UI.CloseUI();
            _isOpen = false;

            if (_currentInteractor != null)
            {
                _currentInteractor.MovementController.SetBusy(false);
                _currentInteractor = null;
            }
        }


        protected virtual void ShowUI()
        {
            _UI.OnCloseButtonClick += StopInteraction;
        }
    }
}
