using Core.Shared;
using Core.Shared.Systems;
using Gameplay.ClientsSystem;
using Gameplay.Orders;
using Gameplay.Tables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Clients 
{
    public class ClientsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _clientsSpawnPoint;
        [SerializeField] private Transform _clientsGoingOutPoint;
        
        private ClientsSpawner_Data _data;
        private ClientsFactory _factory;

        private OrdersManager _ordersManager;
        private Coroutine _spawnCoroutine;

        private float _currentTime = 0f;
        private float _timeTillNextSpawn = 0f;

        private Table _assignedTable;


        public UnityAction OnClientSpawned;


        public void Initialize(ClientsSpawner_Data spawnerData, ClientsFactory_Data factoryData)
        {
            _data = spawnerData;
            _factory = new ClientsFactory(factoryData);
            _ordersManager = SystemsLocator.GetSystem<OrdersSystem>().Manager;
        }


        private Client SpawnClient(Seat assignedSeat, Order assignedOrder)
        {
            CommonClient newClient = _factory.GetNewClient();
            newClient.transform.position = _clientsSpawnPoint.position;
            newClient.transform.rotation = _clientsSpawnPoint.rotation;
            newClient.Initialize(assignedSeat, assignedOrder, _clientsGoingOutPoint);

            return newClient;
        }


        public void SpawnNewClient(int amount, Table assignedTable = null, bool resume = false)
        {
            if (_spawnCoroutine != null) return;

            if (!resume)
            {
                _currentTime = 0;
                SetNewTimeTillNextSpawn();
                _assignedTable = assignedTable;
            }


            _spawnCoroutine = StartCoroutine(SpawnAfterTime(amount));
        }
        
        public void StopSpawn()
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        private IEnumerator SpawnAfterTime(int clientsAmount)
        {
            while (_currentTime < _timeTillNextSpawn)
            {
                yield return null;
                _currentTime += Time.deltaTime;
            }

            _currentTime = 0;


            List<Client> newClients = new List<Client>();

            for (int i =0; i< clientsAmount; i++)
            {
                newClients.Add(SpawnClient(_assignedTable.Seats[i], _ordersManager.GetNewOrder()));
            }
                
            _assignedTable.TakeTable(newClients.ToArray());
            
            _spawnCoroutine = null;
            OnClientSpawned?.Invoke();
        }


        private void SetNewTimeTillNextSpawn()
        {
            _timeTillNextSpawn = Random.Range(_data.MinTimeBetweenSpawn, _data.MaxTimeBetweenSpawn);
        }
    }
}