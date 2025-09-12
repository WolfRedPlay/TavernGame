using Core.Shared;
using Core.Shared.States;
using Core.Shared.Systems;
using Gameplay.Tray;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Gameplay.Tables
{
    public class TableStateManager : StateManager<TableState>
    {
        private TableInteractable _interactable;
        private Table _table;

        private List<Client> _clients = new List<Client>();
        private int _playersSatCount = 0;


        public event UnityAction OnAllClientsSatDown;

        public Table Table=> _table;


        public void Initialize(TableInteractable interactable, List<Client> clientsList, Table table)
        {
            _interactable = interactable;
            _interactable.OnInteract += Interact;
            
            _clients = clientsList;

            _table = table;

            InitializeStatesMap();

            SetState<FreeState>();
        }

        private void Interact()
        {
            _currentState.OnInteract();
        }

        protected override void InitializeStatesMap()
        {
            TrayManager trayManager = SystemsLocator.GetSystem<TraySystem>().Manager;

            StatesMap = new Dictionary<Type, TableState>
            {
                { typeof(FreeState), new FreeState(this) },
                { typeof(WaitingState), new WaitingState(this, _clients, trayManager) },
                { typeof(EatingState), new EatingState(this, _clients) },
            };
        }


        public void SubscribeForClientsSatDown()
        {
            _playersSatCount = 0;

            foreach (var client in _clients)
                client.OnSatDown += ClientSatDown;
        }

        private void ClientSatDown()
        {
            _playersSatCount++;

            if (_playersSatCount == _clients.Count)
            {
                foreach (var client in _clients)
                    client.OnSatDown -= ClientSatDown;

                OnAllClientsSatDown?.Invoke();
            }
        }


        public void FreeTable()
        {
            _table.FreeUpTable();
        }
    }
}