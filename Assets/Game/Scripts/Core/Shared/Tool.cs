using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    public abstract class Tool: MonoBehaviour
    {
        [SerializeField] protected List<Item_Data> _toolItemsData;


        public List<Item_Data> ItemsData => _toolItemsData;


        public abstract Dictionary<Item_Data, int> GetItemsAvailableAmount();

        public abstract int GetItemAvailableAmount(Item_Data item);
        
        public abstract void AddItem(Item_Data itemToAdd, int amount);
    }
}
