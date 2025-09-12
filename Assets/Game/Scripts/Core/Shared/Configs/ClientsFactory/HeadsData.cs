using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Shared 
{
    [CreateAssetMenu(fileName = "HeadsData", menuName = "Configs/ClientsFactory/HeadsData")]
    public class HeadsData : ScriptableObject
    {
        [SerializeField] private List<SkinnedMeshRenderer> _headPrefabs;
        [SerializeField] private List<Gradient> _possibleColors;
        [SerializeField] private Material _hairColorMaterial;


        public List<SkinnedMeshRenderer> HeadPrefabs => _headPrefabs;
        public List<Gradient> PossibleColors => _possibleColors;
        public Material HairColorMaterial => _hairColorMaterial;
    }
}