using Core.Shared.States;

namespace Gameplay.Clients
{
    public abstract class ClientState : IState
    {
        protected ClientsStateManager _stateManager;


        public abstract void OnStart();

        public abstract void OnStop();

        public abstract void OnUpdate();

        public ClientState(ClientsStateManager stateManager)
        {
            _stateManager = stateManager;
        }
    }
}
