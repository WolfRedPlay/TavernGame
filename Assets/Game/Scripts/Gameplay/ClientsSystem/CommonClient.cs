using Core.Shared;
using Gameplay.Tables;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Clients
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ClientMovementController))]
    [RequireComponent(typeof(ClientsStateManager))]
    [RequireComponent(typeof(ClientAnimator))]
    public class CommonClient : Client
    {
        private ClientAnimator _animator;

        private Seat _assignedSeat;
        private Transform _goingOutPoint;


        public ClientAnimator Animator => _animator;
        public Seat AssignedSeat => _assignedSeat;
        public Transform GoingOutPoint => _goingOutPoint;


        public void Initialize(Seat assignedSeat, Order assignedOrder, Transform goingOutPoint)
        {
            _assignedSeat = assignedSeat;
            _assignedOrder = assignedOrder;
            _goingOutPoint = goingOutPoint;

            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            ClientMovementController movementController = GetComponent<ClientMovementController>();
            ClientsStateManager stateManager = GetComponent<ClientsStateManager>();
            _animator = GetComponent<ClientAnimator>();


            movementController.Initialize(agent);

            _animator.Initialize(_assignedSeat);

            stateManager.Initialize(movementController, this);
        }


        public void Despawn()
        {
            Destroy(gameObject);
        }
    }
}