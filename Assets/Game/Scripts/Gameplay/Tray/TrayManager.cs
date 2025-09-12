using Core.Events;
using Core.Shared;
using Core.Shared.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Tray
{
    public class TrayManager : Manager
    {
        private TrayRepository _repository;
        private TraySkin _skin;


        public event UnityAction OnTrayFullFilled;


        public bool HasSpace => _repository.ItemsAmount < _skin.ItemsLimit;


        public TrayManager(TrayRepository repository, TraySkin skin)
        {
            _repository = repository;
            _skin = skin;
        }


        public override void OnCreated()
        {
            EventManager.AddListener<AddItemToTrayEvent>(OnItemAddOnTray);
        }

        public override void Initialize()
        {
            _skin.Initialize();
        }

        public override void OnStarted()
        {
        }


        private void OnItemAddOnTray(AddItemToTrayEvent evt)
        {
            AddItemOnTray(evt.ItemToAdd, evt.IsPerfect);
        }

        public void AddItemOnTray(Item_Data itemToAdd, bool isPerfect)
        {
            if (HasSpace)
            {
                _repository.AddItem(itemToAdd, isPerfect);
                _skin.AddItemOnTray(itemToAdd);
            }

            if (!HasSpace) OnTrayFullFilled?.Invoke();
        }


        public List<Item_Data> GetTrayItems()
        {
            return _repository.GetAvailableItemsList();
        }


        public void TakeItemFromTray(Item_Data itemToTake)
        {
            if (_repository.TryGetItemFromTray(itemToTake))
            {
                _skin.RemoveItemFromTray(itemToTake);
            }
        }
    }
}