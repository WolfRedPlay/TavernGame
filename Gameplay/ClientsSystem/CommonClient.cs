using Core.Shared;
using Core.Shared.ClientsData;
using Gameplay.Tables;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Clients
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ClientMovementController))]
    [RequireComponent(typeof(ClientsStateManager))]
    public class CommonClient : Client
    {
        private Seat _assignedSeat;
        private Transform _goingOutPoint;


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


            movementController.Initialize(agent);

            stateManager.Initialize(movementController, this);
        }


        public void Despawn()
        {
            Destroy(gameObject);
        }
    }
}