using Core.Events;
using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Orders;
using Gameplay.Tables;
using UnityEngine;

namespace Gameplay.Clients
{
    public class ClientsManager : Manager
    {
        private ClientsRepository _repository;
        private ClientsSpawner _spawner;
        private TablesManager _tablesManager;

        private bool _isSpawning = false;
        private Table _freeTable;
        private int _lastClientsAmount;


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
            _spawner.Initialize(_repository.SpawnerData, _repository.FactoryData);
            _tablesManager = SystemsLocator.GetSystem<TablesSystem>().Manager;
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
            int clientsAmount = Random.Range(1, 3);

            for (int i = clientsAmount; i > 0; i--)
            {
                if (IsPossibleToSpawn(i))
                {
                    _lastClientsAmount = i;
                    _spawner.SpawnNewClient(i, _freeTable);
                    break;
                }
            }
        }
        
        private void TryToResumeSpawnClients()
        {
            if (IsPossibleToSpawn(_lastClientsAmount))
                _spawner.SpawnNewClient(_lastClientsAmount, resume: true);
        }


        private bool IsPossibleToSpawn(int clientsAmount)
        {
            // TODO -- Check if possible to spawn
            if (!_tablesManager.TryGetFreeTable(clientsAmount, out _freeTable))
                return false;

            return true;
        }
    }
}