using Core.Events;
using Core.Shared;
using Core.Shared.Items;
using Core.Shared.Systems;
using Gameplay.Tools;
using System.Collections.Generic;
using UI.Orders;
using UnityEngine;

namespace Gameplay.Orders
{
    public class OrdersManager : Manager
    {
        private OrdersRepository _repository;
        private ToolsManager _toolsManager;
        private UIOrdersPool _poolOrdersUI;


        public OrdersManager(OrdersRepository repository)
        {
            _repository = repository;
        }


        public override void OnCreated()
        {
            EventManager.AddListener<AddOrderUIEvent>(AddUIOrder);
            EventManager.AddListener<RemoveOrderUIEvent>(RemoveUIOrder);

            UIOrder UIOrderPrefab = Resources.Load<UIOrder>("Prefabs/UIOrder");

            _poolOrdersUI = new UIOrdersPool(UIOrderPrefab);
        }


        public override void Initialize()
        {
            _toolsManager = SystemsLocator.GetSystem<ToolsSystem>().Manager;
        }

        public override void OnStarted()
        {
        }


        public Order GetNewOrder()
        {
            List<Item_Data> allAvailableItems = _toolsManager.GetAllAvailableItems();

            List<Item_Data> beersData = allAvailableItems.FindAll(x => x is Beer_Data);

            List<Item_Data> foodsData = new List<Item_Data>();

            foreach (Item_Data item in allAvailableItems)
            {
                foodsData.Add(item);
            }

            foodsData.RemoveAll(x => beersData.Contains(x));
            
            List<Item_Data> items = new List<Item_Data>
            {
                beersData.GetRandom(),
                foodsData.GetRandom()
            };

            return new Order(items);
        }


        private void AddUIOrder(AddOrderUIEvent evt)
        {
            UIOrder newOrderUI = _poolOrdersUI.Pool.Get();

            Dictionary<Item_Data, int> orderedItemsMap = new Dictionary<Item_Data, int>();

            foreach (Order order in evt.Orders)
            {
                foreach (var (item, _) in order.OrderedItems)
                {
                    if (orderedItemsMap.ContainsKey(item))
                        orderedItemsMap[item]++;
                    else
                        orderedItemsMap[item] = 1;
                }
            }

            newOrderUI.Initialize(orderedItemsMap, evt.TableTransform);

            _repository.AddNewOrderUI(newOrderUI, evt.TableId);
        }


        private void RemoveUIOrder(RemoveOrderUIEvent evt)
        {
            UIOrder orderToRemove = _repository.TakeOrderFromMap(evt.TableId);
            _repository.RemoveOrderUI(evt.TableId);

            orderToRemove.RemoveOrderUI(_poolOrdersUI.Pool);
        }
    }
}