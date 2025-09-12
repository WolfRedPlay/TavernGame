using Core.Shared;
using UI.Shop;
using UnityEngine;

namespace Gameplay.Interactables.Shop 
{
    [RequireComponent(typeof(Shop))]
    [RequireComponent(typeof(Animator))]
    public class ShopInteractable : InteractableUI<UI_Shop>
    {
        private Shop _shop;
        private Animator _animator;


        private const string OpenTrigger = "Open";
        private const string CloseTrigger = "Close";


        public override void Initialize()
        {
            base.Initialize();

            _shop = GetComponent<Shop>();
            _shop.Initialize();

            _animator = GetComponent<Animator>();
        }


        public override void Interact(Interactor interactor)
        {
            if (_isOpen) return;
            _isOpen = true;
            _currentInteractor = interactor;
            _animator.SetTrigger(OpenTrigger);
            
            if (_currentInteractor != null) _currentInteractor.MovementController.SetBusy(true);
        }


        protected override void ShowUI()
        {
            base.ShowUI();

            _UI.OpenUI(_shop.GetAvailableItemsMap());
            _UI.OnTryBuyItem += OnTryBuyItem;
        }

        private void OnTryBuyItem(Item_Data data, int amount)
        {
            if (_shop.TryBuyItem(data, amount))
                _UI.ConfirmPurchase(data);
        }


        public override void StopInteraction()
        {
            base.StopInteraction();
            _UI.OnTryBuyItem -= OnTryBuyItem;
            _animator.SetTrigger(CloseTrigger);
        }


        public void OnOpenAnimationFinished()
        {
            ShowUI();
        }
    }
}