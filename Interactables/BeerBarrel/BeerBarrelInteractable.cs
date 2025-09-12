using Core.Events;
using UnityEngine;


namespace Gameplay.Interactables.BeerBarrel
{
    [RequireComponent(typeof(Barrel))]
    public class BeerBarrelInteractable : ZoomInteractable
    {
        private Barrel _barrel;
        private bool _isOpen = false;


        public override void Initialize()
        {
            base.Initialize();

            _barrel = GetComponent<Barrel>();
        }


        public override void Interact()
        {
            if (_isOpen) return;

            base.Interact();

            EventManager.AddListener<CameraBlendingFinishedEvent>(ShowUI);

            _isOpen = true;
        }


        private void ShowUI(CameraBlendingFinishedEvent evt)
        {
            EventManager.RemoveListener<CameraBlendingFinishedEvent>(ShowUI);

            EventManager.Broadcast(new BeerBarrelMinigameOpenEvent(_barrel.FillValue,_barrel.BeerData.PerfectArea));
            
            _barrel.SubscribeOnButtonEvents();
            
            EventManager.AddListener<BeerBarrelMinigameCloseEvent>(StopInteraction);
        }


        public void StopInteraction(BeerBarrelMinigameCloseEvent evt)
        {
            EventManager.RemoveListener<BeerBarrelMinigameCloseEvent>(StopInteraction);
            _barrel.UnsubscribeFromButtonEvents();

            ZoomOut();

            _isOpen = false;
        }
    }
}