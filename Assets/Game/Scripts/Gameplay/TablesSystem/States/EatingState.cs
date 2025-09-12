using Core.Shared;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Tables
{
    public class EatingState : TableState
    {
        private List<Client> _clients;
        private int _finishedClients = 0;


        public EatingState(TableStateManager stateManager, List<Client> clientsList) : base(stateManager)
        {
            _clients = clientsList;
        }

        public override void OnInteract()
        {
        }

        public override void OnStart()
        {
            _finishedClients = 0;

            foreach (Client client in _clients)
                client.OnEatFinished += ClientFinished;
        }

        private void ClientFinished()
        {
            _finishedClients++;

            if (_finishedClients == _clients.Count)
                _stateManager.SetState<FreeState>();
        }

        public override void OnStop()
        {
            foreach (Client client in _clients)
            {
                client.OnEatFinished -= ClientFinished;
                client.TriggerGetOut();
            }

            _stateManager.FreeTable();
        }

        public override void OnUpdate()
        {
        }
    }
}
