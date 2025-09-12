using Core.Events;
using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Orders;
using Gameplay.Tables;

namespace Gameplay.Clients
{
    public class ClientsManager : Manager
    {
        private ClientsRepository _repository;
        private ClientsSpawner _spawner;
        private TablesManager _tablesManager;
        private OrdersManager _ordersManager;

        private bool _isSpawning = false;
        private Table _freeTable;


        public ClientsManager(ClientsRepository repository, ClientsSpawner spawner) 
        {
            _repository = repository;    
            _spawner = spawner;
        }


        public override void OnCreated()
        {
        }
        
        public override void Initialize()
        {
            _spawner.Initialize(_repository.SpawnerData);
            _tablesManager = SystemsLocator.GetSystem<TablesSystem>().Manager;
            _ordersManager = SystemsLocator.GetSystem<OrdersSystem>().Manager;
        }

        public override void OnStarted()
        {
            StartClientsSpawn();
        }


        public void StartClientsSpawn(bool resume = false)
        {
            if (_isSpawning) return;
            _isSpawning = true;
            _spawner.OnClientSpawned += TryToSpawnClient;

            if (!resume)
                TryToSpawnClient();
            else
                TryToResumeSpawnClients();
            EventManager.AddListener<FreeTableEvent>(OnTableFree);
        }

        public void StopClientsSpawn()
        {
            if (!_isSpawning) return;
            _isSpawning = false;

            _spawner.StopSpawn();
            _spawner.OnClientSpawned -= TryToSpawnClient;
            EventManager.RemoveListener<FreeTableEvent>(OnTableFree);
        }


        private void OnTableFree(FreeTableEvent evt)
        {
            TryToSpawnClient();
        }

        private void TryToSpawnClient()
        {
            if (IsPossibleToSpawn())
            {
                Order newOrder = _ordersManager.GetNewOrder();

                _spawner.SpawnNewClient(_freeTable, newOrder);
            }
        }
        
        private void TryToResumeSpawnClients()
        {
            if (IsPossibleToSpawn())
                _spawner.SpawnNewClient(resume : true);
        }


        private bool IsPossibleToSpawn()
        {
            // TODO -- Check if possible to spawn
            if (!_tablesManager.TryGetFreeTable(1, out _freeTable))
                return false;

            return true;
        }
    }
}