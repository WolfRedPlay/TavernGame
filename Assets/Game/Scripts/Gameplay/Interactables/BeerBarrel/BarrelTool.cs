using Core.Events;
using Core.Shared;
using Core.Shared.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interactables.BeerBarrel
{
    public class BarrelTool : Tool
    {
        private ObservableValue<float> _fillValue = new ObservableValue<float>(0);
        private bool _isFilling = false;


        private int _beerAmount = 0;

        private const int MaxBeerAmount = 10;


        public Beer_Data BeerData => _toolItemsData[0] as Beer_Data;
        public ObservableValue<float> FillValue => _fillValue;


        private void Update()
        {
            if (_isFilling)
            {
                _fillValue.Value += BeerData.FillSpeed * Time.deltaTime;
                if (_fillValue.Value > 1f) StopFilling();
            }
        }


        public void SubscribeOnButtonEvents()
        {
            EventManager.AddListener <OnFillingButtonDownEvent>(OnFillingButtonDown);
            EventManager.AddListener <OnFillingButtonUpEvent>(OnFillingButtonUp);
        }
        
        public void UnsubscribeFromButtonEvents()
        {
            EventManager.RemoveListener<OnFillingButtonDownEvent>(OnFillingButtonDown);
            EventManager.RemoveListener<OnFillingButtonUpEvent>(OnFillingButtonUp);
        }


        private void OnFillingButtonDown(OnFillingButtonDownEvent evt)
        {
            if (_beerAmount == 0) return;
            StartFilling();
        }
        
        private void OnFillingButtonUp(OnFillingButtonUpEvent evt)
        {
            if (_beerAmount == 0) return;
            StopFilling();
        }


        public void StartFilling()
        {
            _fillValue.Value = 0;
            _isFilling = true;
        }

        public void StopFilling()
        {
            if (!_isFilling) return;
            _isFilling = false;

            if (BeerData.PerfectArea.IsValueInArea(_fillValue.Value))
            {
                // TODO -- Perfect filling logic

                EventManager.Broadcast(new AddItemToTrayEvent(BeerData, true));

                Debug.Log("Filled perfect!!!");
            }
            else if (BeerData.GoodArea.IsValueInArea(_fillValue.Value))
            {
                // TODO -- Common filling logic

                EventManager.Broadcast(new AddItemToTrayEvent(BeerData, false));

                Debug.Log("Filled!");
            }
            else
            {
                // TODO -- Fail logic
                Debug.Log("Failed");
            }

            _beerAmount--;
            _fillValue.Value = 0;
        }

        private void OnValidate()
        {
            if (_toolItemsData.Count > 1)
            {
                _toolItemsData.RemoveRange(1, _toolItemsData.Count - 1);
            }

            if (_toolItemsData.Count == 1 && !(_toolItemsData[0] is Beer_Data))
            {
                _toolItemsData.RemoveAt(0);
            }
        }

        public override Dictionary<Item_Data, int> GetItemsAvailableAmount()
        {
            Dictionary<Item_Data, int> amounts = new Dictionary<Item_Data, int>
            {
                { _toolItemsData[0], MaxBeerAmount - _beerAmount }
            };
            
            return amounts;
        }


        public override void AddItem(Item_Data itemToAdd, int amount)
        {
            _beerAmount += amount;
        }

        public override int GetItemAvailableAmount(Item_Data item)
        {
            if (!_toolItemsData.Contains(item)) return 0;

            return MaxBeerAmount - _beerAmount;
        }
    }
}