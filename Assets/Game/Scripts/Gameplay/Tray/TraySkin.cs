using Core.Shared;
using Gameplay.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Tray
{
    public class TraySkin : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _itemPositionsParent;
        [SerializeField] private GameObject _skin;

        private List<Transform> _itemPositionPoints;
        private List<GameObject> _itemsOnTray = new List<GameObject>();


        public int ItemsLimit => _itemPositionPoints.Count;


        public void Initialize()
        {
            _itemPositionPoints = new List<Transform>();

            foreach (Transform point in _itemPositionsParent)
                _itemPositionPoints.Add(point);

            HideTray();
        }


        public void ShowTray()
        {
            _skin.SetActive(true);
            _playerAnimator.ShowTray();
        }
        
        public void HideTray()
        {
            _skin.SetActive(false);
            _playerAnimator.HideTray();
        }


        public void AddItemOnTray(Item_Data itemToAdd)
        {
            foreach (Transform itemPoint in _itemPositionPoints)
            {
                if (itemPoint.childCount == 0)
                {
                    GameObject newItem = Instantiate(itemToAdd.SkinPrefab,itemPoint);
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localRotation = Quaternion.identity;
                    newItem.layer = gameObject.layer;
                    newItem.name = itemToAdd.Name;
                    if (_itemsOnTray.Count == 0)
                        ShowTray();
                    _itemsOnTray.Add(newItem);
                    return;
                }
            }    
        }
        
        public void RemoveItemFromTray(Item_Data itemToAdd)
        {
            GameObject itemToRemove = _itemsOnTray.Find(x => x.name == itemToAdd.Name);
            _itemsOnTray.Remove(itemToRemove);
            Destroy(itemToRemove);
            if (_itemsOnTray.Count == 0)
                HideTray();
        }
    }
}
