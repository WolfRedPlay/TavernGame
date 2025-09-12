using Core.Events;
using Core.Shared;
using Core.Shared.ClientsData;
using Gameplay.Tray;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Tables
{
    public class WaitingState : TableState
    {
        private List<Client> _clients;
        private TrayManager _trayManager;


        public WaitingState(TableStateManager stateManager, List<Client> clientsList, TrayManager trayManager) : base(stateManager)
        {
            _clients = clientsList;
            _trayManager = trayManager;
        }

        public override void OnInteract()
        {
            foreach (Client client in _clients)
            {
                List<Item_Data> itemsOnTray = _trayManager.GetTrayItems();

                List<Item_Data> commonItems = itemsOnTray.Where(x => client.AssignedOrder.OrderedItems
                .Any(order => (order.item == x && !order.isDone))).ToList();

                foreach (Item_Data commonItem in commonItems)
                {
                    _trayManager.TakeItemFromTray(commonItem);
                    client.AssignedOrder.FinishOrderPart(commonItem);
                }
            }

            if (CheckIfOrdersDone())
                FinishOrders();
        }

        public override void OnStart()
        {
            EventManager.Broadcast(new AddOrderUIEvent(_stateManager.Table.ID, _stateManager.Table.transform, _clients.Select(c => c.AssignedOrder).ToList()));
        }

        public override void OnStop()
        {
            EventManager.Broadcast(new RemoveOrderUIEvent(_stateManager.Table.ID));
        }

        public override void OnUpdate()
        {
        }


        private bool CheckIfOrdersDone()
        {
            bool checker = true;

            foreach (Client client in _clients)
            {
                if (!client.AssignedOrder.IsDone())
                {
                    checker = false;
                    break;
                }
            }

            return checker;
        }

        private void FinishOrders()
        {
            foreach (Client client in _clients)
                client.TriggerGetOrder();

            _stateManager.SetState<EatingState>();
        }
    }
}
