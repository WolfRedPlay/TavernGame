using Core.Shared;
using Core.Shared.ClientsData;
using Gameplay.Tables;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Clients 
{
    public class ClientsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _clientsSpawnPoint;
        [SerializeField] private Transform _clientsGoingOutPoint;
        
        private ClientsSpawner_Data _data;
        private Coroutine _spawnCoroutine;

        private float _currentTime = 0f;
        private float _timeTillNextSpawn = 0f;

        private Table _assignedTable;
        private Order _assignedOrder;


        public UnityAction OnClientSpawned;


        public void Initialize(ClientsSpawner_Data newData)
        {
            _data = newData;
        }


        private Client SpawnClient(Seat assignedSeat, Order assignedOrder)
        {
            GameObject newGO = Instantiate(_data.ClientPrefab, _clientsSpawnPoint.position, _clientsSpawnPoint.rotation);
            CommonClient newClient = newGO.GetComponent<CommonClient>();
            newClient.Initialize(assignedSeat, assignedOrder, _clientsGoingOutPoint);

            return newClient;
        }


        public void SpawnNewClient(Table assignedTable = null, Order assignedOrder = new Order(), bool resume = false)
        {
            if (_spawnCoroutine != null) return;

            if (!resume)
            {
                _currentTime = 0;
                SetNewTimeTillNextSpawn();
                _assignedTable = assignedTable;
                _assignedOrder = assignedOrder;
            }


            _spawnCoroutine = StartCoroutine(SpawnAfterTime());
        }
        
        public void StopSpawn()
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        private IEnumerator SpawnAfterTime()
        {
            while (_currentTime < _timeTillNextSpawn)
            {
                yield return null;
                _currentTime += Time.deltaTime;
            }

            _currentTime = 0;


            Client newClient = SpawnClient(_assignedTable.Seats[0], _assignedOrder);
            _assignedTable.TakeTable(newClient);
            
            _spawnCoroutine = null;
            OnClientSpawned?.Invoke();
        }


        private void SetNewTimeTillNextSpawn()
        {
            _timeTillNextSpawn = Random.Range(_data.MinTimeBetweenSpawn, _data.MaxTimeBetweenSpawn);
        }
    }
}