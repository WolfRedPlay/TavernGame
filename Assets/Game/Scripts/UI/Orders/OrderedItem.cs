using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Orders
{
    public class OrderedItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;

        public void Initialize(Sprite icon, int amount)
        {
            _icon.sprite = icon;
            _amountText.text = $"X {amount}";
        }
    }
}