using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gameplay.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Space]
        [Header("Right Hand")]
        [SerializeField] private MultiAimConstraint _aimRH;
        [SerializeField] private MultiPositionConstraint _positionRH;
        [SerializeField] private MultiRotationConstraint _rotationRH;


        private const float MinBlinkTime = 2.5f;
        private const float MaxBlinkTime = 4.5f;
        private const string Walking = "Walking";
        private const string Blink = "Blink";


        public void Initialize()
        {
            HideTray();

            if ( _animator == null)
            {
                Debug.LogWarning("Animator for Player was not assigned!!!");
                enabled = false;
                return;
            }

            StartCoroutine(BlinkCoroutine(Random.Range(MinBlinkTime, MaxBlinkTime)));
        }


        private IEnumerator BlinkCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            _animator.SetTrigger(Blink);

            StartCoroutine(BlinkCoroutine(Random.Range(MinBlinkTime, MaxBlinkTime)));
        }


        public void SetWalking(bool isWalking)
        {
            if (!enabled) return;

            _animator.SetBool(Walking, isWalking);
        }


        public void ShowTray()
        {
            var srcAim = _aimRH.data.sourceObjects;
            srcAim.SetWeight(0, 1f);
            _aimRH.data.sourceObjects = srcAim;

            var srcPos = _positionRH.data.sourceObjects;
            srcPos.SetWeight(0, 1f);
            _positionRH.data.sourceObjects = srcPos;

            var srcRot = _rotationRH.data.sourceObjects;
            srcRot.SetWeight(0, 1f);
            _rotationRH.data.sourceObjects = srcRot;
        }

        public void HideTray()
        {
            var srcAim = _aimRH.data.sourceObjects;
            srcAim.SetWeight(0, 0f);
            _aimRH.data.sourceObjects = srcAim;

            var srcPos = _positionRH.data.sourceObjects;
            srcPos.SetWeight(0, 0f);
            _positionRH.data.sourceObjects = srcPos;

            var srcRot = _rotationRH.data.sourceObjects;
            srcRot.SetWeight(0, 0f);
            _rotationRH.data.sourceObjects = srcRot;
        }
    }
}