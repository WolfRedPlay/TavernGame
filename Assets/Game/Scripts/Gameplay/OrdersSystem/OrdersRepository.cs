using Core.Shared.Systems;
using System.Collections.Generic;
using UI.Orders;

namespace Gameplay.Orders
{
    public class OrdersRepository : Repository
    {
        Dictionary<uint, UIOrder> _ordersUIMap = new Dictionary<uint, UIOrder>();


        public override void OnCreated()
        {
        }
        
        public override void Initialize()
        {
        }

        public override void OnStarted()
        {
        }


        public void AddNewOrderUI(UIOrder newOrderUI, uint tableId)
        {
            _ordersUIMap[tableId] = newOrderUI;
        }

        public UIOrder TakeOrderFromMap(uint tableId)
        {
            return _ordersUIMap[tableId];
        }

        public void RemoveOrderUI(uint tableId)
        {
            _ordersUIMap.Remove(tableId);
        }
    }
}