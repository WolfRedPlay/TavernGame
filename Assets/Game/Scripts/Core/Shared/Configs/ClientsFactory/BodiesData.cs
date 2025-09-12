using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "BodiesData", menuName = "Configs/ClientsFactory/BodiesData")]

    public class BodiesData : ScriptableObject
    {
        [SerializeField] private List<BodyData> _bodies;
        [SerializeField] private Material _bodyColorMaterial;


        public List<BodyData> Bodies => _bodies;
        public Material BodyColorMaterial => _bodyColorMaterial;
    }


    [Serializable]
    public struct BodyData 
    {
        [SerializeField] private GameObject _bodyPrefab;
        [SerializeField] private List<Gradient> _possibleColors;


        public GameObject Prefab => _bodyPrefab;
        public List<Gradient> PossibleColors => _possibleColors;
    }

}
