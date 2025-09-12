using Core.Shared;
using Core.Shared.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Tray
{
    public class TrayRepository : Repository
    {
        private Dictionary<string, TrayItem_Data> _itemsContainer = new Dictionary<string, TrayItem_Data>();


        public int ItemsAmount => GetItemsAmount();


        public override void OnCreated()
        {
        }
        
        public override void Initialize()
        {
        }

        public override void OnStarted()
        {
        }


        public void AddItem(Item_Data itemToAdd, bool isPerfect)
        {
            if (_itemsContainer.ContainsKey(itemToAdd.Name))
            {
                _itemsContainer[itemToAdd.Name].AddItem();
            }
            else
            {
                _itemsContainer.Add(itemToAdd.Name, new TrayItem_Data(itemToAdd, isPerfect));
            }

            Debug.Log($"[Tray]: Item added on tray : {itemToAdd.Name}  {_itemsContainer[itemToAdd.Name].Amount}");
        }

        public bool TryGetItemFromTray(Item_Data itemToTake)
        {
            if (_itemsContainer.ContainsKey(itemToTake.Name))
            {
                TrayItem_Data changedItem = _itemsContainer[itemToTake.Name];
                changedItem.RemoveItem();
                _itemsContainer[itemToTake.Name] = changedItem;

                if (_itemsContainer[itemToTake.Name].Amount == 0)
                    _itemsContainer.Remove(itemToTake.Name);

                return true;
            }
            else
                return false;
        }


        public List<Item_Data> GetAvailableItemsList()
        {
            List<Item_Data> availableItems = new List<Item_Data>();

            foreach (TrayItem_Data trayItem in _itemsContainer.Values)
                availableItems.Add(trayItem.ItemData);

            return availableItems;
        }


        private int GetItemsAmount()
        {
            int amount = 0;

            foreach(TrayItem_Data item in _itemsContainer.Values)
            {
                amount += item.Amount;
            }

            return amount;
        }
    }
}
