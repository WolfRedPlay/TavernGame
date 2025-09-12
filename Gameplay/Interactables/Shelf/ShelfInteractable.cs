using Core.Events;
using UI;
using UnityEngine;

namespace Gameplay.Interactables.Shelf 
{
    [RequireComponent(typeof(Shelf))]
    public class ShelfInteractable : ZoomInteractable<ShelfUI>
    {
        private Shelf _shelf;

        public override void Initialize()
        {
            base.Initialize();

            _shelf = GetComponent<Shelf>();
            _shelf.Initialize();
        }


        public override void Interact()
        {
            base.Interact();
        }


        protected override void OnCameraBlendingFinished(CameraBlendingFinishedEvent evt)
        {
            base.OnCameraBlendingFinished(evt);
            _UI.OpenUI();
            _shelf.SetItemsActive(true);
        }


        public override void StopInteraction()
        {
            base.StopInteraction();
            _shelf.SetItemsActive(false);
        }
    }
}