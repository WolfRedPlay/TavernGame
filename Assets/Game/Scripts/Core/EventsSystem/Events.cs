using Core.Shared;
using System.Collections.Generic;
using UnityEngine;
// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

namespace Core.Events
{
    public static class Events
    {
    }


    public class MovePlayerEvent : GameEvent
    {
        public Transform Target { get; private set; }
        public IInteractable Interactable { get; private set; }

        public bool RotateAtTheEnd { get; private set; }


        public MovePlayerEvent(Transform target, IInteractable interactable, bool rotateAtTheEnd)
        {
            Target = target;
            Interactable = interactable;
            RotateAtTheEnd = rotateAtTheEnd;
        }
    }

    public class PathFinishedEvent : GameEvent { }


    public class HidePlayerEvent : GameEvent { }

    public class ShowPlayerEvent : GameEvent { }


    public class CameraBlendingFinishedEvent : GameEvent { }


    public class OnFillingButtonDownEvent: GameEvent { }

    public class OnFillingButtonUpEvent: GameEvent { }


    public class AddItemToTrayEvent: GameEvent 
    {
        private Item_Data _itemToAdd;
        private bool _isPerfect;

        public Item_Data ItemToAdd => _itemToAdd;
        public bool IsPerfect => _isPerfect;


        public AddItemToTrayEvent(Item_Data newItem, bool isPerfect)
        {
            _itemToAdd = newItem;
            _isPerfect = isPerfect;
        }
    }


    public class FreeTableEvent : GameEvent { }


    public class AddOrderUIEvent: GameEvent 
    {
        private uint _tableId;
        private Transform _tableTransform;
        private List<Order> _orders;

        public uint TableId => _tableId;
        public Transform TableTransform => _tableTransform;
        public List<Order> Orders => _orders;


        public AddOrderUIEvent(uint tableId, Transform tableTransform, List<Order> orders)
        {
            _tableId = tableId;
            _tableTransform = tableTransform;
            _orders = orders;
        }
    }

    public class RemoveOrderUIEvent: GameEvent 
    {
        private uint _tableId;

        public uint TableId => _tableId;


        public RemoveOrderUIEvent(uint tableId)
        {
            _tableId = tableId;
        }
    }



}