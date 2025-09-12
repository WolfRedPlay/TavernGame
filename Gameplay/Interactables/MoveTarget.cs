using Core.Events;
using Core.Shared;
using UnityEngine;

namespace Gameplay.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class MoveTarget : MonoBehaviour, IMoveTarget
    {
        [SerializeField] private Transform _standPoint;
        [SerializeField] private bool _rotateAtTheEnd = true;


        public Transform StandPoint => _standPoint;


        public virtual void Initialize()
        {
            if (_standPoint == null)
            {
                Debug.LogWarning("Move To Interactable " + gameObject.name + " doesn't have direction point!");
                enabled = false;
                return;
            }
        }


        private void OnMouseDown()
        {
            CallPlayerToMove();
        }


        public void CallPlayerToMove()
        {
            IInteractable interactable = (this is IInteractable) ? this as IInteractable : null;

            EventManager.Broadcast(new MovePlayerEvent(_standPoint, interactable));
        }
    }
}