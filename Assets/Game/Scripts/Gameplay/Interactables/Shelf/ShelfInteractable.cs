using Core.Events;
using UI;
using UnityEngine;

namespace Gameplay.Interactables.Shelf 
{
    [RequireComponent(typeof(ShelfTool))]
    public class ShelfInteractable : ZoomInteractable<UI_Shelf>
    {
        [SerializeField] private Collider _triggerCollider;

        private ShelfTool _shelf;


        public override void Initialize()
        {
            base.Initialize();

            _shelf = GetComponent<ShelfTool>();
            _shelf.Initialize();
        }


        protected override void ShowUI()
        {
            base.ShowUI();
            _UI.OpenUI();
            _shelf.SetItemsActive(true);
            _triggerCollider.enabled = false;
        }


        public override void StopInteraction()
        {
            base.StopInteraction();
            _shelf.SetItemsActive(false);
            _triggerCollider.enabled = true;
        }
    }
}