using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Interactables.Shelf
{
    public class Sausages_Shelf : MonoBehaviour
    {
        Shelf _assignedShelf;
        List<Collider> _colliders;


        public void Initialize(Rigidbody shelfRB, Shelf shelf)
        {
            GetComponent<SpringJoint>().connectedBody = shelfRB;
            _assignedShelf = shelf;
            _colliders = new List<Collider>();

            _colliders = GetComponentsInChildren<Collider>().ToList();
            SetActive(false);
        }


        public void SetActive(bool active)
        {
            foreach (var collider in _colliders)
                collider.enabled = active;
        }


        private void OnMouseDown()
        {
            _assignedShelf.TryToTakeSausages(this);
        }
    }
}