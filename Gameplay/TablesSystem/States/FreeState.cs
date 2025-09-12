using UnityEngine;

namespace Gameplay.Tables
{
    public class FreeState : TableState
    {
        public FreeState(TableStateManager stateManager) : base(stateManager)
        {
        }

        public override void OnInteract()
        {
        }

        public override void OnStart()
        {
            _stateManager.OnAllClientsSatDown += _stateManager.SetState<WaitingState>;
        }

        public override void OnUpdate()
        {
        }

        public override void OnStop()
        {
            _stateManager.OnAllClientsSatDown -= _stateManager.SetState<WaitingState>;
        }
    }
}
