using Core.Events;
using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Tray;
using UI;
using UnityEngine;


namespace Gameplay.Interactables.BeerBarrel
{
    [RequireComponent(typeof(BarrelTool))]
    public class BeerBarrelInteractable : ZoomInteractable<UI_BeerBarrel>
    {
        private BarrelTool _barrel;
        private TrayManager _trayManager;


        public override void Initialize()
        {
            base.Initialize();

            _trayManager = SystemsLocator.GetSystem<TraySystem>().Manager;
            _barrel = GetComponent<BarrelTool>();
        }


        public override void Interact(Interactor interactor)
        {
            if (!_trayManager.HasSpace) return;

            base.Interact(interactor);
        }


        public override void StopInteraction()
        {
            _trayManager.OnTrayFullFilled -= StopInteraction;
            _barrel.UnsubscribeFromButtonEvents();

            base.StopInteraction();
        }


        protected override void ShowUI()
        {
            base.ShowUI();
            _trayManager.OnTrayFullFilled += StopInteraction;

            _UI.OpenUI(_barrel.BeerData.GoodArea, _barrel.BeerData.PerfectArea, _barrel.FillValue);
            
            _barrel.SubscribeOnButtonEvents();
        }
    }
}