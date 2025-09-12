
using Core.Shared;
using UnityEngine;

namespace Gameplay.Interactables
{
    public class InteractableUI<T>: MoveTarget, IInteractable where T : UI_Interactable
    {
        protected T _UI;
        protected bool _isOpen = false;


        public override void Initialize()
        {
            base.Initialize();

            _UI = FindAnyObjectByType<T>(FindObjectsInactive.Include);
        }


        public virtual void Interact()
        {
            if (_isOpen) return;
            ShowUI();
            _isOpen = true;
        }

        public virtual void StopInteraction()
        {
            _UI.OnCloseButtonClick -= StopInteraction;
            _UI.CloseUI();
            _isOpen = false;
        }


        protected virtual void ShowUI()
        {
            _UI.OnCloseButtonClick += StopInteraction;
        }
    }
}
