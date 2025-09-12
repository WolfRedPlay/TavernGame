using System;
using System.Collections.Generic;

namespace Core.Shared.Systems
{
    public static class SystemsLocator
    {
        private static Dictionary<Type, ISystem> _systemsMap = new Dictionary<Type, ISystem>(); 


        public static void RegisterSystem<T>(T system) where T : ISystem
        {
            Type type = typeof(T);

            if (_systemsMap.ContainsKey(type))
                throw new Exception("Trying to register system, which is already registered!!!");

            _systemsMap[type] = system;
        }

        public static void UnregisterSystem<T>() where T : ISystem
        {
            Type type = typeof(T);

            if (_systemsMap.ContainsKey(type))
                throw new Exception("Trying to unregister system, which is not registered!!!");

            _systemsMap.Remove(type);
        }


        public static void CreateAllSystems()
        {
            foreach (var system in _systemsMap.Values)
            {
                system.Create();
            }
        }
        
        public static void InitializeAllSystems()
        {
            foreach (var system in _systemsMap.Values)
            {
                system.Initialize();
            }
        }

        public static void StartAllSystems()
        {
            foreach (var system in _systemsMap.Values)
            {
                system.Start();
            }
        }


        public static T GetSystem<T> () where T : ISystem
        {
            return (T)_systemsMap[typeof(T)];
        }
    }
}
