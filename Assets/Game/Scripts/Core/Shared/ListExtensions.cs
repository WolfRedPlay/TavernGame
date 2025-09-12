using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new System.Exception("Get random element is not possible for empty of null list!!!");

            int index = Random.Range(0, list.Count);

            return list[index];
        }
    }
}
