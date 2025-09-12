using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Core.Shared
{
    public abstract class MovementController : MonoBehaviour
    {
        protected NavMeshAgent _agent;
        protected Transform _target;
        protected bool _rotateToTarget = true;
        protected bool _isBusy = false;


        public event UnityAction OnMovementStarted;
        public event UnityAction OnMovementFinished;


        public virtual void Initialize(NavMeshAgent agent)
        {
            _agent = agent;
        }


        public virtual void MoveToTarget(Transform newTarget)
        {
            if (_isBusy) return;

            _target = newTarget;
            _agent.destination = _target.position;

            OnMovementStarted?.Invoke();
        }


        protected virtual void Update()
        {
            if (!_agent.hasPath && _target != null)
            {
                if (!_rotateToTarget) FinishMovement();
                 else RotateToTarget();
            }
        }


        protected virtual void RotateToTarget()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _agent.angularSpeed / 20 * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, _target.rotation) <= 4)
            {
                transform.rotation = _target.rotation;

                FinishMovement();
            }
        }


        protected virtual void FinishMovement()
        {
            _target = null;
            OnMovementFinished?.Invoke();
        }


        public void SetAgentActive(bool active)
        {
            _agent.enabled = active;
        }


        public void SetBusy(bool busy)
        {
            _isBusy = busy;
        }


        public void SetAgentPriority(int priority)
        {
            _agent.avoidancePriority = priority;
        }
    }
}
