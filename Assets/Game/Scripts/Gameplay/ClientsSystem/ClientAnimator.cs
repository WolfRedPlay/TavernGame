using Gameplay.Tables;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;


namespace Gameplay.Clients
{
    [RequireComponent(typeof(Animator))]
    public class ClientAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _handTarget;
        [SerializeField] private Transform _bodyTarget;

        [SerializeField] private MultiAimConstraint _aimConstraint;
        [SerializeField] private MultiPositionConstraint _positionConstraint;
        [SerializeField] private MultiRotationConstraint _rotationConstraint;


        private Animator _animator;
        private Seat _assignedSeat;
        
        private Transform _handStartParent;
        private Vector3 _handStartLocalPosition;
        private Quaternion _handStartLocalRotation;

        private Transform _bodyStartParent;
        private Vector3 _bodyStartLocalPosition;
        private Quaternion _bodyStartLocalRotation;

        private bool _handOnSeat = false;
        private bool _handOnSeatBack = false;

        private Blinking _blinking;


        public event UnityAction OnSitDown;
        public event UnityAction OnStandUp;

        private const float MinBlinkTime = 2.5f;
        private const float MaxBlinkTime = 4.5f;
        private const float MinHeadRotateDelay = 7f;
        private const float MaxHeadRotateDelay = 14f;

        private const string Walking = "Walking";
        private const string Eating = "Eating";
        private const string SitDownTrigger = "SitDown";
        private const string StandUpTrigger = "StandUp";
        private const string HeadRotationTrigger = "RotateHead";
        private const string Blink = "Blink";



        public void Initialize(Seat assignedSeat)
        {
            _handStartParent = _handTarget.parent;
            _handStartLocalPosition = _handTarget.localPosition;
            _handStartLocalRotation = _handTarget.localRotation;

            _bodyStartParent = _bodyTarget.parent;
            _bodyStartLocalPosition = _bodyTarget.localPosition;
            _bodyStartLocalRotation = _bodyTarget.localRotation;

            _animator = GetComponent<Animator>();
            _assignedSeat = assignedSeat;

            _blinking = new Blinking(this, _animator);

            StartHeadRotation();
        }


        public void StartHeadRotation()
        {
            StartCoroutine(HeadRotationCoroutine(Random.Range(MinHeadRotateDelay, MaxHeadRotateDelay)));
        }

        private IEnumerator HeadRotationCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            _animator.SetTrigger(HeadRotationTrigger);
        }


        public void SetWalking(bool walking)
        {
            _animator.SetBool(Walking, walking);
        }

        public void SetEating(bool walking)
        {
            _animator.SetBool(Eating, walking);
        }


        public void StartSitDownAnimation()
        {
            _animator.SetTrigger(SitDownTrigger);
            MoveHandOnSeatBack();
        }

        private void SitDown()
        {
            _assignedSeat.OnMovementFinished -= SitDown;

            _animator.SetTrigger("SitDown");
        }

        public void FinishSitDown()
        {
            ResetBodyTarget();
            ResetHandTarget();

            OnSitDown?.Invoke();
        }


        public void StartStandUpAnimation()
        {
            MoveBodyFromSeat();
            MoveHandOnSeat();

            _animator.SetTrigger(StandUpTrigger);
        }

        public void FinishStandUp()
        {
            ResetBodyTarget();
            ResetHandTarget();

            OnStandUp?.Invoke();
        }


        public void MoveSeat(float distance)
        {
            _assignedSeat.OnMovementFinished += SitDown;
            StartCoroutine(_assignedSeat.Move(distance));
        }


        public void MoveHandOnSeat()
        {
            _handOnSeat = true;
            _handOnSeatBack = false;

            _handTarget.rotation = _assignedSeat.HandPointSeat.rotation;
        }

        public void MoveHandOnSeatBack()
        {
            _handOnSeat = false;
            _handOnSeatBack = true;

            _handTarget.rotation = _assignedSeat.HandPointBack.rotation;
        }

        public void ResetHandTarget()
        {
            _handOnSeat = false;
            _handOnSeatBack = false;

            _handTarget.localPosition = _handStartLocalPosition;
            _handTarget.localRotation = _handStartLocalRotation;
        }


        public void MoveBodyOnSeat()
        {
            _bodyTarget.forward = _assignedSeat.SeatPoint.right;

            Vector3 ToVector = _assignedSeat.SeatPoint.position - _bodyTarget.position;

            float distance = Vector3.Dot(ToVector, _bodyTarget.right);

            Vector3 targetPosition = _bodyTarget.position + _bodyTarget.right * distance;

            _bodyTarget.position = targetPosition;
        }

        public void MoveBodyFromSeat()
        {
            _bodyTarget.rotation = _assignedSeat.ApproachPoint.rotation;
            _bodyTarget.position = _assignedSeat.ApproachPoint.position;
        }

        public void ResetBodyTarget()
        {
            _bodyTarget.SetParent(_bodyStartParent);

            _bodyTarget.localPosition = _bodyStartLocalPosition;
            _bodyTarget.localRotation = _bodyStartLocalRotation;
        }


        private void Update()
        {
            if (_handOnSeat)
                _handTarget.position = _assignedSeat.HandPointSeat.position;

            if (_handOnSeatBack)
                _handTarget.position = _assignedSeat.HandPointBack.position;
        }
    }
}