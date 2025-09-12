using Core.Events;
using Core.Shared.Systems;
using Gameplay.Tray;
using UI;
using UnityEngine;


namespace Gameplay.Interactables.BeerBarrel
{
    [RequireComponent(typeof(Barrel))]
    public class BeerBarrelInteractable : ZoomInteractable<BeerBarrelUI>
    {
        private Barrel _barrel;
        private TrayManager _trayManager;


        public override void Initialize()
        {
            base.Initialize();

            _trayManager = SystemsLocator.GetSystem<TraySystem>().Manager;
            _barrel = GetComponent<Barrel>();
        }


        public override void Interact()
        {
            if (!_trayManager.HasSpace) return;

            base.Interact();
        }


        public override void StopInteraction()
        {
            _trayManager.OnTrayFullFilled -= StopInteraction;
            _barrel.UnsubscribeFromButtonEvents();

            base.StopInteraction();
        }


        protected override void ShowUI()
        {
            _trayManager.OnTrayFullFilled += StopInteraction;

            _UI.OpenUI(_barrel.BeerData.GoodArea, _barrel.BeerData.PerfectArea, _barrel.FillValue);
            
            _barrel.SubscribeOnButtonEvents();
        }
    }
}