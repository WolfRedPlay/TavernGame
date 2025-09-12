using UnityEngine;

namespace Core.Shared
{
    public interface IMoveTarget
    {
        public Transform StandPoint { get; }

        public void CallPlayerToMove();
    }
}
