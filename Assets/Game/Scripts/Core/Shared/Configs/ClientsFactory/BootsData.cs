using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "BootsData", menuName = "Configs/ClientsFactory/BootsData")]
    public class BootsData : ScriptableObject
    {
        [SerializeField] private SkinnedMeshRenderer _bootsPrefab;
        [SerializeField] private List<Gradient> _possibleColors;


        public SkinnedMeshRenderer BootsPrefab => _bootsPrefab;
        public List<Gradient> PossibleColors => _possibleColors;
    }
}
