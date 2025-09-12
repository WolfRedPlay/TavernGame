using Cinemachine;
using Core.Events;
using UnityEngine;

namespace Gameplay.Camera
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineBrain _cameraBrain;
        private bool _wasBlending = false;

        public void Initialize()
        {
            _cameraBrain = GetComponent<CinemachineBrain>();
        }


        void Update()
        {
            if (_cameraBrain.IsBlending)
            {
                _wasBlending = true;
            } else if (_wasBlending)
            {
                _wasBlending = false;

                EventManager.Broadcast(new CameraBlendingFinishedEvent());
            }
        }
    }
}