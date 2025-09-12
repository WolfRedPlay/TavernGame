using Cinemachine;
using Core.Events;
using Core.Shared;
using UnityEngine;


namespace Gameplay.Interactables
{
    public class ZoomInteractable<T>: InteractableUI<T>  where T : UI_Interactable
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


        public override void Interact(Interactor interactor)
        {
            if (_isOpen) return;
            ZoomIn();

            EventManager.AddListener<CameraBlendingFinishedEvent>(OnCameraBlendingFinished);

            _isOpen = true;

            _currentInteractor = interactor;

            if (_currentInteractor != null) _currentInteractor.MovementController.SetBusy(true);
        }

        public override void StopInteraction()
        {
            base.StopInteraction();

            ZoomOut();
        }


        protected virtual void OnCameraBlendingFinished(CameraBlendingFinishedEvent evt)
        {
            EventManager.RemoveListener<CameraBlendingFinishedEvent>(OnCameraBlendingFinished);

            ShowUI();
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