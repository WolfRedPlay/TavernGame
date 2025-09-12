using UnityEngine;

namespace Gameplay.Tables
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Transform _approachPoint;
        [SerializeField] private Transform _seatPoint;

        public Transform ApproachPoint => _approachPoint;
        public Transform SeatPoint => _seatPoint;
    }
}