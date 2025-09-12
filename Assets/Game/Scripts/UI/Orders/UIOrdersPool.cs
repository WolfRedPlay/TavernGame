using UnityEngine;
using UnityEngine.Pool;

namespace UI.Orders
{
    public class UIOrdersPool
    {
        private IObjectPool<UIOrder> _pool;

        private const int MaxPoolSize = 10;
        private UIOrder _orderPrefab;


        public UIOrdersPool(UIOrder prefab)
        {
            _orderPrefab = prefab;
        }


        public IObjectPool<UIOrder> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<UIOrder>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, MaxPoolSize);
                }
                return _pool;
            }
        }

        UIOrder CreatePooledItem()
        {
            UIOrder newOrderUI = GameObject.Instantiate(_orderPrefab);
            newOrderUI.name = "OrderUI";

            return newOrderUI;
        }

        void OnReturnedToPool(UIOrder order)
        {
            order.gameObject.SetActive(false);
        }

        void OnTakeFromPool(UIOrder order)
        {
            order.gameObject.SetActive(true);
        }

        void OnDestroyPoolObject(UIOrder order)
        {
            GameObject.Destroy(order.gameObject);
        }
    }
}