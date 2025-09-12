using Core.Shared;

namespace Gameplay.Tray
{
    public class TrayItem_Data
    {
        private Item_Data _itemData;
        private int _amount;
        private bool _isPerfect;

        public Item_Data ItemData => _itemData;
        public int Amount => _amount;
        public bool IsPerfect => _isPerfect;


        public TrayItem_Data(Item_Data data, bool isPerfect)
        {
            _itemData = data;
            _amount = 1;
            _isPerfect = isPerfect;
        }

        public void AddItem()
        {
            _amount++;
        }
        
        public void RemoveItem()
        {
            _amount--;
        }
    }
}