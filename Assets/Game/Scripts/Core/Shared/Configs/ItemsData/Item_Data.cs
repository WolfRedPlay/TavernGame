using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    [CreateAssetMenu(fileName = "Item", menuName = "Configs/Item Data")]
    public class Item_Data : ScriptableObject, IEqualityComparer<Item_Data>
    {
        [Header("General")]
        [SerializeField] private string _name;
        [SerializeField][TextArea] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _skinPrefab;

        [Space]
        [Header("Prices")]
        [SerializeField] private int _buyPrice;
        [SerializeField] private int _sellPrice;

        [Space]
        [SerializeField][Range(.5f, 20)] private float _eatDuration;


        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public GameObject SkinPrefab => _skinPrefab;
        public int BuyPrice => _buyPrice;
        public int SellPrice => _sellPrice;
        public float EatDuration => _eatDuration;


        public bool Equals(Item_Data x, Item_Data y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Name == y.Name;
        }

        public int GetHashCode(Item_Data obj)
        {
            return obj.Name?.GetHashCode() ?? 0;
        }
    }
}
