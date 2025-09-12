using Core.Events;
using Core.Shared;
using Core.Shared.Items;
using UnityEngine;

namespace Gameplay.Interactables.BeerBarrel
{
    public class Barrel : Tool
    {
        private ObservableValue<float> _fillValue = new ObservableValue<float>(0);
        private bool _isFilling = false;


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
            StartFilling();
        }
        
        private void OnFillingButtonUp(OnFillingButtonUpEvent evt)
        {
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

            if (_fillValue.Value >= 1f)
            {
                // TODO -- Fail logic
                Debug.Log("Failed");
            }
            else if (BeerData.PerfectArea.IsValueInArea(_fillValue.Value))
            {
                // TODO -- Perfect filling logic
                Debug.Log("Filled perfect!!!");
            }
            else
            {
                // TODO -- Common filling logic
                Debug.Log("Filled!");
            }

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
    }
}