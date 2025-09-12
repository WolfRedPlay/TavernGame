using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Tables
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Transform _approachPoint;
        [SerializeField] private Transform _seatPoint;
        [SerializeField] private Transform _handPointBack;
        [SerializeField] private Transform _handPointSeat;


        public event UnityAction OnMovementFinished;


        public Transform ApproachPoint => _approachPoint;
        public Transform SeatPoint => _seatPoint;
        public Transform HandPointSeat => _handPointSeat;
        public Transform HandPointBack => _handPointBack;


        public IEnumerator Move(float distance)
        {
            float currentDistance = 0;

            Vector3 endPosition = transform.localPosition + transform.right * distance;

            while (Mathf.Abs(currentDistance) < Mathf.Abs(distance))
            {
                float distanceChange = 3f * Time.deltaTime;
                transform.localPosition += transform.right * Mathf.Sign(distance) * distanceChange;
                currentDistance += distanceChange;
                yield return null;
            }

            transform.localPosition = endPosition;
            OnMovementFinished?.Invoke();
            yield return null;
        }
    }
}