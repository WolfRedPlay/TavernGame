using Core.Events;
using Core.Shared;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI 
{
    public class UI_BeerBarrel : UI_Interactable
    {
        [SerializeField] private FillSliderController _fillingArea;
        [SerializeField] private EventTrigger _buttonEventTrigger;


        public override void Initialize()
        {
            base.Initialize();

            AddTriggers();
        }

        
        public void OpenUI(FloatAreaOnRange goodArea, FloatAreaOnRange perfectArea, ObservableValue<float> fillValue)
        {
            _fillingArea.UpdateGoodArea(goodArea);
            _fillingArea.UpdatePerfectArea(perfectArea);
            _fillingArea.StartTrackSliderValue(fillValue);

            _rootObject.SetActive(true);
        }


        public override void CloseUI()
        {
            base.CloseUI();

            _fillingArea.StopTrackSliderValue();
        }


        private void AddTriggers()
        {
            EventTrigger.Entry newPointerDown = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };

            newPointerDown.callback.AddListener(OnHoldButtonDown);

            EventTrigger.Entry newPointerUp = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };

            newPointerUp.callback.AddListener(OnHoldButtonUp);

            _buttonEventTrigger.triggers.Add(newPointerDown);
            _buttonEventTrigger.triggers.Add(newPointerUp);
        }


        public void OnHoldButtonDown(BaseEventData data)
        {
            EventManager.Broadcast(new OnFillingButtonDownEvent());
        }
        
        public void OnHoldButtonUp(BaseEventData data)
        {
            EventManager.Broadcast(new OnFillingButtonUpEvent());
        }
    }
}


