using System.Collections;
using UnityEngine;

namespace Core.Shared
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject newGo = new GameObject("COROUTINES MANAGER");
                    _instance = newGo.AddComponent<Coroutines>();
                    DontDestroyOnLoad(newGo);
                }

                return _instance;
            }
        }

        private static Coroutines _instance;


        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return Instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}