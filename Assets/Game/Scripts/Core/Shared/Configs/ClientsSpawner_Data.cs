using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "ClientsSpawner_Data", menuName = "Configs/ClientsSpawner")]
    public class ClientsSpawner_Data : ScriptableObject
    {
        [SerializeField][Range(0, 20)] private int _minTimeBetweenSpawn = 10;
        [SerializeField][Range(0, 20)] private int _maxTimeBetweenSpawn = 10;


        public int MinTimeBetweenSpawn => _minTimeBetweenSpawn;
        public int MaxTimeBetweenSpawn => _maxTimeBetweenSpawn;


        private void OnValidate()
        {
            if (_minTimeBetweenSpawn > _maxTimeBetweenSpawn)
                _minTimeBetweenSpawn = _maxTimeBetweenSpawn;

            if (_maxTimeBetweenSpawn < _minTimeBetweenSpawn)
                _maxTimeBetweenSpawn = _minTimeBetweenSpawn;
        }
    }
}
