using Cinemachine;
using Core.Events;
using Core.Shared;
using UnityEngine;


namespace Gameplay.Interactables
{
    public class ZoomInteractable : MoveTarget, IInteractable
    {
        [SerializeField] private CinemachineVirtualCamera _camera;


        public override void Initialize()
        {
            base.Initialize();

            if (_camera == null)
            {
                Debug.LogWarning("Zoom Interactable " + gameObject.name + " doesn't have the camera!");
                enabled = false;
                return;
            }

            ZoomOut();
        }


        public virtual void Interact()
        {
            ZoomIn();
        }


        private void ZoomIn()
        {
            _camera.gameObject.SetActive(true);
            EventManager.Broadcast(new HidePlayerEvent());
        }

        public void ZoomOut()
        {
            _camera.gameObject.SetActive(false);
            EventManager.Broadcast(new ShowPlayerEvent());
        }
    }
}

