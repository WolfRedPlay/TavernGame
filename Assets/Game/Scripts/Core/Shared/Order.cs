using System.Collections.Generic;

namespace Core.Shared
{
    public struct Order
    {
        private List<(Item_Data item,bool isDone)> _orderedItems;

        public List<(Item_Data item, bool isDone)> OrderedItems => _orderedItems;


        public Order(List<Item_Data> OrderedItems)
        {
            _orderedItems = new List<(Item_Data item, bool isDone)>();
            foreach (var item in OrderedItems)
                _orderedItems.Add((item, false));
        }


        public bool IsDone()
        {
            bool checker = true;

            foreach (var item in _orderedItems)
                if (!item.isDone)
                {
                    checker = false;
                    break;
                }

            return checker;
        }

        public void FinishOrderPart(Item_Data itemToFinish)
        {
            int index = _orderedItems.FindIndex(x => x.item == itemToFinish);

            if (index != -1)
                _orderedItems[index] = (_orderedItems[index].item, true);
        }
    }
}