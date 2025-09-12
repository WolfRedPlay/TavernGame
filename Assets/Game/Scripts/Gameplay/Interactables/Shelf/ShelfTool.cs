using Core.Events;
using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Tray;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Interactables.Shelf
{
    public class ShelfTool : Tool
    {
        [SerializeField] Sausages_Shelf _sausagesPrefab;
        [SerializeField] Cheese_Shelf _cheesePrefab;

        [SerializeField] Transform _hooksParent;
        [SerializeField] Transform _cheesePointsParents;


        Item_Data _sausagesData;
        Item_Data _cheeseData;

        Rigidbody _rb;
        TrayManager _trayManager;
        List<Transform> _hookTransforms = new List<Transform>();
        List<Transform> _cheesePoints = new List<Transform>();

        List<Sausages_Shelf> _sausagesOnShelf = new List<Sausages_Shelf>();
        List<Cheese_Shelf> _cheeseOnShelf = new List<Cheese_Shelf>();


        public readonly int MaxSausagesOnHook = 3;
        public readonly int MaxCheeseAmount = 12;


        public void Initialize()
        {
            foreach (Transform hook in _hooksParent)
                _hookTransforms.Add(hook);

            foreach (Transform point in _cheesePointsParents)
                _cheesePoints.Add(point);
            
            _rb = GetComponent<Rigidbody>();
            _trayManager = SystemsLocator.GetSystem<TraySystem>().Manager;

            _sausagesData = _toolItemsData.Find(x => x.Name == "Sausages");
            _cheeseData = _toolItemsData.Find(x => x.Name == "Cheese");
        }


        public void AddNewSausages()
        {
            Transform chosenHook = _hookTransforms[0];

            for(int i = 1; i < _hookTransforms.Count; i++)
            {
                if (_hookTransforms[i].childCount < chosenHook.childCount)
                    chosenHook = _hookTransforms[i];
            }

            Sausages_Shelf newSausages = Instantiate(_sausagesPrefab, chosenHook);
            newSausages.Initialize(_rb, this);
            _sausagesOnShelf.Add(newSausages);
        }

        public void TryToTakeSausages(Sausages_Shelf sausagesToRemove)
        {
            if (!_trayManager.HasSpace) return;

            _sausagesOnShelf.Remove(sausagesToRemove);
            Destroy(sausagesToRemove.gameObject);

            EventManager.Broadcast(new AddItemToTrayEvent(_sausagesData, false));
        }


        public void AddNewCheese()
        {
            Transform chosenPoint = _cheesePoints[_cheeseOnShelf.Count];

            Cheese_Shelf newCheese = Instantiate(_cheesePrefab, chosenPoint);
            newCheese.Initialize(this);
            _cheeseOnShelf.Add(newCheese);
        }

        public void TryToTakeCheese()
        {
            if (!_trayManager.HasSpace) return;

            Cheese_Shelf cheeseToRemove = _cheeseOnShelf[_cheeseOnShelf.Count - 1];
            
            _cheeseOnShelf.Remove(cheeseToRemove);
            Destroy(cheeseToRemove.gameObject);

            EventManager.Broadcast(new AddItemToTrayEvent(_cheeseData, false));
        }


        public void SetItemsActive(bool active)
        {
            foreach (Sausages_Shelf sausages in _sausagesOnShelf)
                sausages.Active = active;

            foreach (Cheese_Shelf cheese in _cheeseOnShelf)
                cheese.Active = active;
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                AddNewSausages();

            if (Input.GetKeyDown(KeyCode.C))
                AddNewCheese();
        }

        public override Dictionary<Item_Data, int> GetItemsAvailableAmount()
        {
            Dictionary<Item_Data, int> amounts = new Dictionary<Item_Data, int>
            {

                { _sausagesData, (MaxSausagesOnHook * _hookTransforms.Count) - _sausagesOnShelf.Count },
                { _cheeseData, _cheesePoints.Count - _cheeseOnShelf.Count }
            };

            return amounts;
        }


        public override void AddItem(Item_Data itemToAdd, int amount)
        {
            if (itemToAdd == _sausagesData)
                for (int i = 0; i < amount; i++)
                    AddNewSausages();

            if (itemToAdd == _cheeseData)
                for (int i = 0; i < amount; i++)
                    AddNewCheese();
        }

        public override int GetItemAvailableAmount(Item_Data item)
        {
            if (!_toolItemsData.Contains(item)) return 0;

            if (item == _sausagesData)
                return (_hookTransforms.Count * MaxSausagesOnHook) - _sausagesOnShelf.Count;
            else
                return MaxCheeseAmount - _cheeseOnShelf.Count;
        }
    }
}