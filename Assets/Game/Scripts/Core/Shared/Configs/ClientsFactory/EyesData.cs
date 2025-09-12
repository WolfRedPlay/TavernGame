using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "EyesData", menuName = "Configs/ClientsFactory/EyesData")]
    public class EyesData : ScriptableObject
    {
        [SerializeField] private SkinnedMeshRenderer _eyesPrefab;
        [SerializeField] private List<Gradient> _possibleColors;
        [SerializeField] private Material _colorMaterial;


        public SkinnedMeshRenderer EyesPrefab => _eyesPrefab;
        public List<Gradient> PossibleColors => _possibleColors;
        public Material ColorMaterial => _colorMaterial;
    }
}
