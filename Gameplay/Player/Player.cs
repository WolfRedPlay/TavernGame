using Core.Events;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Player 
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _modelRootTransform;

        private VisibilityController _visibilityController;
        private PlayerMovementController _movementController;
        private PlayerAnimator _animator;


        public void Initialize()
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<PlayerAnimator>();

            _visibilityController = new VisibilityController(_modelRootTransform);

            if (!TryGetComponent(out _movementController))
                _movementController = gameObject.AddComponent<PlayerMovementController>();

            _movementController.Initialize(agent);
            _animator.Initialize();

            _movementController.OnMovementStarted += OnMovementStarted;
            _movementController.OnMovementFinished += OnMovementFinished;


            EventManager.AddListener<HidePlayerEvent>((evt) => _visibilityController.Hide());
            EventManager.AddListener<ShowPlayerEvent>((evt) => _visibilityController.Show());
        }


        private void OnMovementStarted()
        {
            _animator.SetWalking(true);
        }

        private void OnMovementFinished()
        {
            _animator.SetWalking(false);
        }


        private void OnDestroy()
        {
            EventManager.RemoveListener<HidePlayerEvent>((evt) => _visibilityController.Hide());
            EventManager.RemoveListener<ShowPlayerEvent>((evt) => _visibilityController.Show());

            _movementController.OnMovementStarted -= OnMovementStarted;
            _movementController.OnMovementFinished -= OnMovementFinished;
        }
    }
}