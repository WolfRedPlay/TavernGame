using Core.Shared.States;

namespace Gameplay.Tables 
{
    public abstract class TableState : IState
    {
        protected TableStateManager _stateManager;


        public abstract void OnStart();

        public abstract void OnStop();

        public abstract void OnUpdate();

        public abstract void OnInteract();


        public TableState(TableStateManager stateManager)
        {
            _stateManager = stateManager;
        }
    }
}


