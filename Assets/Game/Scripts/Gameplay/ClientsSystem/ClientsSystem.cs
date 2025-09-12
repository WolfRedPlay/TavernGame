using Core.Shared.Systems;

namespace Gameplay.Clients
{
    public class ClientsSystem : GameSystem<ClientsRepository, ClientsManager>
    {
        private ClientsSpawner _spawner;


        public ClientsSystem(ClientsSpawner spawner) 
        {
            _spawner = spawner;
        }


        protected override ClientsManager CreateManager()
        {
            return new ClientsManager(Repository, _spawner);
        }
    }
}
