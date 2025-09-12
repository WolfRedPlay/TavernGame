using Core.Events;
using Core.Shared;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Tables 
{
    [RequireComponent(typeof(TableStateManager))]
    [RequireComponent(typeof(TableInteractable))]
    public class Table : MonoBehaviour
    {
        [SerializeField] private List<Seat> _seats;

        private List<Client> _clients = new List<Client>();
        private bool _isBusy = false;
        private TableStateManager _stateManager;
        private uint _tableId;


        public bool IsBusy => _isBusy;
        public List<Seat> Seats => _seats;
        public uint ID => _tableId;


        public void Initialize(uint id)
        {
            _tableId = id;

            TableInteractable interactable = GetComponent<TableInteractable>();
            _stateManager = GetComponent<TableStateManager>();

            _stateManager.Initialize(interactable, _clients, this);
        }


        public void TakeTable(params Client[] clients)
        {
            foreach (Client client in clients)
                _clients.Add(client);

            _isBusy = true;
            _stateManager.SubscribeForClientsSatDown();
        }

        public void FreeUpTable()
        {
            _clients.Clear();
            _isBusy = false;
            EventManager.Broadcast(new FreeTableEvent());
        }
    }
}