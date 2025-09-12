using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "ClientsFactory_Data", menuName = "Configs/ClientsFactory/Factory")]
    public class ClientsFactory_Data : ScriptableObject
    {
        [SerializeField] private HeadsData _headsData;
        [SerializeField] private BodiesData _bodiesData;
        [SerializeField] private BootsData _bootsData;
        [SerializeField] private EyesData _eyesData;

        [Space]
        [SerializeField] private GameObject _clientPrefab;


        public HeadsData HeadsData => _headsData;
        public BodiesData BodiesData => _bodiesData;
        public BootsData BootsData => _bootsData;
        public EyesData EyesData => _eyesData;
        public GameObject ClientPrefab => _clientPrefab;
    }
}
