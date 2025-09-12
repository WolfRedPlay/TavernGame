using Core.Shared;
using Core.Shared.Systems;
using UnityEngine;

namespace Gameplay.Clients
{
    public class ClientsRepository : Repository
    {
        private ClientsSpawner_Data _clientSpawnerData;
        private ClientsFactory_Data _clientFactoryData;


        public ClientsSpawner_Data SpawnerData => _clientSpawnerData;
        public ClientsFactory_Data FactoryData => _clientFactoryData;


        public override void OnCreated()
        {
            _clientSpawnerData = Resources.Load<ClientsSpawner_Data>("Configs/ClientsSpawner_Data");
            _clientFactoryData = Resources.Load<ClientsFactory_Data>("Configs/ClientsFactory_Data");
        }

        public override void Initialize()
        {
        }


        public override void OnStarted()
        {
        }
    }
}
