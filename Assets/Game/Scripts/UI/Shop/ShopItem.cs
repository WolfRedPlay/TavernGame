using Core.Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_InputField _amountField;

        [Space]
        [Header("Buttons")]
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _buyButton;


        private UI_Shop _shopUI;
        private Item_Data _data;
        private int _maxAmount;
        private int _amount;

        
        public Item_Data Data => _data;


        public void Initialize(Item_Data itemData, int maxAmount, UI_Shop shopUI)
        {
            _data = itemData;
            _icon.sprite = itemData.Icon;
            _nameText.text = itemData.Name;
            name = itemData.Name;
            _maxAmount = maxAmount;

            _shopUI = shopUI;

            UpdateAmount(0);

            _amountField.onEndEdit.AddListener(UpdateAmount);

            _increaseButton.onClick.AddListener(IncreaseAmount);
            _decreaseButton.onClick.AddListener(DecreaseAmount);
            _buyButton.onClick.AddListener(BuyItem);
        }
        

        public void UpdateAmount(string newAmount)
        {
            UpdateAmount(int.Parse(newAmount));
        }

        private void UpdateAmount(int newAmount)
        {
            if (newAmount > _maxAmount)
            {
                UpdateAmount(_maxAmount);
                return;
            }

            _amount = newAmount;
            _amountField.text = _amount.ToString();
        }

        public void IncreaseAmount()
        {
            if (_amount == _maxAmount) return;

            UpdateAmount(_amount + 1);
        }

        public void DecreaseAmount()
        {
            if (_amount == 0) return;

            UpdateAmount(_amount - 1);
        }


        public void BuyItem()
        {
            _shopUI.TryBuyItem(_data, _amount);
        }


        public void OnPurchaseConfirmed()
        {
            _maxAmount -= _amount;
            UpdateAmount(0);
        }
    }
}