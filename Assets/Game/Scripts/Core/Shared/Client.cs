using UnityEngine;
using UnityEngine.Events;

namespace Core.Shared
{
    public abstract class Client : MonoBehaviour
    {
        [SerializeField] SkinnedMeshRenderer _originalRenderer;
        protected Order _assignedOrder;

        public Order AssignedOrder => _assignedOrder;
        public SkinnedMeshRenderer OriginalRenderer => _originalRenderer;

        public event UnityAction OnSatDown;
        public event UnityAction OnGetOrder;
        public event UnityAction OnEatFinished;
        public event UnityAction OnGettingOut;


        public void TriggerSatDown()
        {
            OnSatDown?.Invoke();
        }

        public void TriggerGetOrder()
        {
            OnGetOrder?.Invoke();
        }

        public void TriggerEatFinished()
        {
            OnEatFinished?.Invoke();
        }

        public void TriggerGetOut()
        {
            OnGettingOut?.Invoke();
        }
    }
}