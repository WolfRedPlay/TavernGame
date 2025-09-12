using Core.Shared;
using System.Linq;
using UnityEngine;

namespace Gameplay.Clients
{
    public class EatingState : ClientState
    {
        private Order _clientOrder;

        private float _eatTime;
        private float _currentTime = 0f;
        private bool _isEating = false;


        public EatingState(ClientsStateManager stateManager, Order clientOrder) : base(stateManager)
        {
            _clientOrder = clientOrder;
        }

        public override void OnStart()
        {
            _eatTime = _clientOrder.OrderedItems.Max(x => x.item.EatDuration);
            _isEating = true;

            _stateManager.SubscribeForClientGetOut();
        }

        public override void OnStop()
        {
            _stateManager.UnsubscribeForClientGetOut();
        }

        public override void OnUpdate()
        {
            if (!_isEating) return;

            _currentTime += Time.deltaTime;

            if (_currentTime >= _eatTime)
            {
                _stateManager.TrigerClientEatFinished();
                _isEating = false;
            }
        }
    }
}
