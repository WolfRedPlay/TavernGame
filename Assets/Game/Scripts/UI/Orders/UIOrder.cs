using Core.Shared;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace UI.Orders
{
    public class UIOrder : MonoBehaviour
    {
        [SerializeField] private RectTransform _orderedItemsParent;
        [SerializeField] private OrderedItem _orderedItemPrefab;


        List<OrderedItem> _currentOrderedItems = new List<OrderedItem>();


        public void Initialize(Dictionary<Item_Data, int> orderedItemsMap, Transform tableTransform)
        {
            transform.SetParent(tableTransform, false);
            transform.localPosition = new Vector3(0, 4f, 0);

            foreach (var orderedItem in orderedItemsMap)
            {
                OrderedItem newItem = Instantiate(_orderedItemPrefab, _orderedItemsParent);
                newItem.Initialize(orderedItem.Key.Icon, orderedItem.Value);
                _currentOrderedItems.Add(newItem);
            }
        }


        public void RemoveOrderUI(IObjectPool<UIOrder> pool)
        {
            transform.SetParent(null);
            ClearOrderedItems();
            pool.Release(this);
        }


        private void ClearOrderedItems()
        {
            foreach (OrderedItem item in _currentOrderedItems)
            {
                Destroy(item.gameObject);
            }

            _currentOrderedItems.Clear();
        }
    }
}