using Core.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Shop
{
    public class UI_Shop : UI_Interactable
    {
        [SerializeField] RectTransform _itemsListContentTransform;
        [SerializeField] ShopItem _itemPrefab;


        List<ShopItem> _activeShopItems = new List<ShopItem>();


        public event Action<Item_Data, int> OnTryBuyItem;


        public void OpenUI(Dictionary<Item_Data, int> items)
        {
            foreach (var item in items)
            {
                ShopItem newItem = Instantiate(_itemPrefab, _itemsListContentTransform);
                newItem.Initialize(item.Key, item.Value, this);
                _activeShopItems.Add(newItem);
            }

            _rootObject.SetActive(true);
        }


        public override void CloseUI()
        {
            base.CloseUI();

            foreach (ShopItem item in _activeShopItems)
            {
                Destroy(item.gameObject);
            }
            _activeShopItems.Clear();

            OnTryBuyItem = null;
        }


        public void TryBuyItem(Item_Data boughtItem, int amount)
        {
            OnTryBuyItem?.Invoke(boughtItem, amount);
        }


        public void ConfirmPurchase(Item_Data boughtItem)
        {
            _activeShopItems.Find(x => x.Data == boughtItem).OnPurchaseConfirmed();
        }

    }
}