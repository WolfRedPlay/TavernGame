using Core.Shared;
using UnityEngine;

namespace Gameplay.Interactables.Shelf
{
    public class Cheese_Shelf : MonoBehaviour
    {
        Shelf _assignedShelf;
        Collider _collider;


        public void Initialize(Shelf shelf)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _assignedShelf = shelf;
            _collider = GetComponent<Collider>();
            SetActive(false);
        }


        public void SetActive(bool active)
        {
            _collider.enabled = active;
        }


        private void OnMouseDown()
        {
            _assignedShelf.TryToTakeCheese();
        }
    }
}