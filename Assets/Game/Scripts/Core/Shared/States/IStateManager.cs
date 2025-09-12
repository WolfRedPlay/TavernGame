using System;

namespace Core.Shared.States
{
    public interface IStateManager<T> where T : IState
    {
        public void SetState<U>() where U : T;
    }
}
