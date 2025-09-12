using Core.Shared.States;
using System;
using System.Collections.Generic;

namespace Gameplay.Clients
{
    public class ClientsStateManager : StateManager<ClientState>
    {
        private CommonClient _client;


        public ClientAnimator Animator => _client.Animator;


        public void Initialize(ClientMovementController movementController, CommonClient client)
        {
            MovementController = movementController;
            _client = client;

            InitializeStatesMap();

            SetState<MovingToSeatState>();
        }

        protected override void InitializeStatesMap()
        {
            StatesMap = new Dictionary<Type, ClientState>
            {
                { typeof(MovingToSeatState), new MovingToSeatState(this, _client.AssignedSeat) },
                { typeof(SeatingState), new SeatingState(this, _client.AssignedSeat) },
                { typeof(WaitingState), new WaitingState(this) },
                { typeof(EatingState), new EatingState(this, _client.AssignedOrder) },
                { typeof(StandingUpState), new StandingUpState(this, _client.AssignedSeat) },
                { typeof(MovingOutState), new MovingOutState(this, _client.GoingOutPoint) }
            };
        }


        public void SubscribeForClientGetOrder()
        {
            _client.OnGetOrder += SetState<EatingState>;
        }
        
        public void UnsubscribeForClientGetOrder()
        {
            _client.OnGetOrder -= SetState<EatingState>;
        }
        
        public void SubscribeForClientGetOut()
        {
            _client.OnGettingOut += SetState<StandingUpState>;
        }
        
        public void UnsubscribeForClientGetOut()
        {
            _client.OnGettingOut -= SetState<StandingUpState>;
        }


        public void TrigerClientEatFinished()
        {
            _client.TriggerEatFinished();
        }


        public void TrigerClientSatDown()
        {
            _client.TriggerSatDown();
        }


        public void DespawnClient()
        {
            _client.Despawn();
        }
    }
}