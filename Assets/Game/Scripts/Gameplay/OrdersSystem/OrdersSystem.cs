using Core.Shared.Systems;

namespace Gameplay.Orders
{
    public class OrdersSystem : GameSystem<OrdersRepository, OrdersManager>
    {
        public OrdersSystem() { }


        protected override OrdersManager CreateManager()
        {
            return new OrdersManager(Repository);
        }
    }
}