using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Interactables.Shelf
{
    public class Sausages_Shelf : MonoBehaviour
    {
        private ShelfTool _assignedShelf;


        public bool Active { get; set; }


        public void Initialize(Rigidbody shelfRB, ShelfTool shelf)
        {
            GetComponent<SpringJoint>().connectedBody = shelfRB;
            _assignedShelf = shelf;
            Active = false;
        }


        private void OnMouseDown()
        {
            if (!Active) return;
            _assignedShelf.TryToTakeSausages(this);
        }
    }
}