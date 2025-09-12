using Core.Shared;
using UnityEngine;

namespace Gameplay.Interactables.Shelf
{
    public class Cheese_Shelf : MonoBehaviour
    {
        private ShelfTool _assignedShelf;
        

        public bool Active { get; set; }

        public void Initialize(ShelfTool shelf)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _assignedShelf = shelf;
            Active = false;
        }


        private void OnMouseDown()
        {
            if (!Active) return;
            _assignedShelf.TryToTakeCheese();
        }
    }
}