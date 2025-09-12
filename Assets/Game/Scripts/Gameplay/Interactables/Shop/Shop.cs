using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interactables.Shop
{
    public class Shop : MonoBehaviour
    {
        private ToolsRepository _toolsRepository;


        public void Initialize()
        {
            _toolsRepository = SystemsLocator.GetSystem<ToolsSystem>().Repository;
        }


        public Dictionary<Item_Data, int> GetAvailableItemsMap()
        {
            Dictionary<Item_Data, int> items = new Dictionary<Item_Data, int>();

            foreach (Tool tool in _toolsRepository.AvailableTools)
            {
                Dictionary<Item_Data, int> toolItems = tool.GetItemsAvailableAmount();
                foreach (var (data, amount) in toolItems)
                {
                    if (!items.ContainsKey(data))
                        items.Add(data, amount);
                    else
                        items[data] += amount;
                }
            }

            return items;
        }


        public bool TryBuyItem(Item_Data itemToBuy, int amount)
        {
            List<Tool> toolsWithItem = new List<Tool>();

            foreach (Tool tool in _toolsRepository.AvailableTools)
                if (tool.ItemsData.Contains(itemToBuy))
                    toolsWithItem.Add(tool);

            foreach (Tool tool in toolsWithItem)
            {
                int availableSlots = tool.GetItemAvailableAmount(itemToBuy);
                int amountToAdd = Math.Min(availableSlots, amount);
                tool.AddItem(itemToBuy, amountToAdd);
                amount -= amountToAdd;
                if (amount <= 0) break;
            }

            return true;
        }
    }
}