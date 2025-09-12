using Core.Shared.ClientsData;
using Core.Shared.Systems;
using UnityEngine;

namespace Gameplay.Clients
{
    public class ClientsRepository : Repository
    {
        private ClientsSpawner_Data _clientSpawnerData;


        public ClientsSpawner_Data SpawnerData => _clientSpawnerData;


        public override void OnCreated()
        {
            _clientSpawnerData = Resources.Load<ClientsSpawner_Data>("Configs/ClientsSpawner_Data");
        }

        public override void Initialize()
        {
        }


        public override void OnStarted()
        {
        }
    }
}
