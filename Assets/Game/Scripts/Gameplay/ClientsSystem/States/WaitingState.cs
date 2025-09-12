namespace Gameplay.Clients
{
    public class WaitingState : ClientState
    {
        public WaitingState(ClientsStateManager stateManager) : base(stateManager)
        {
        }

        public override void OnStart()
        {
            _stateManager.SubscribeForClientGetOrder();
        }

        public override void OnStop()
        {
            _stateManager.UnsubscribeForClientGetOrder();
        }

        public override void OnUpdate()
        {
        }
    }
}
