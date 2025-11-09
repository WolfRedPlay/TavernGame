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


        private Blinking _blinking;

        private const string Walking = "Walking";


        public void Initialize()
        {
            HideTray();

            if ( _animator == null)
            {
                Debug.LogWarning("Animator for Player was not assigned!!!");
                enabled = false;
                return;
            }

            _blinking = new Blinking(this, _animator);
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