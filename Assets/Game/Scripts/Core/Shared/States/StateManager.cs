using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared.States
{
    public abstract class StateManager<T> : MonoBehaviour, IStateManager<T> where T : IState
    {
        protected T _currentState;

        public MovementController MovementController { get; protected set; }
        public Dictionary<Type, T> StatesMap { get; protected set; }


        protected abstract void InitializeStatesMap();


        public virtual void SetState<U>() where U : T
        {
            if (_currentState != null)
                _currentState.OnStop();

            _currentState = StatesMap[typeof(U)];
            _currentState.OnStart();
        }


        protected virtual void Update()
        {
            if (_currentState != null)
                _currentState.OnUpdate();
        }
    }
}