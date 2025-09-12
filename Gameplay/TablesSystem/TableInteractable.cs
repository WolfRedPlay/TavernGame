using Core.Shared;
using Gameplay.Interactables;
using UnityEngine.Events;

namespace Gameplay.Tables
{
    public class TableInteractable : MoveTarget, IInteractable
    {
        public event UnityAction OnInteract;


        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}
