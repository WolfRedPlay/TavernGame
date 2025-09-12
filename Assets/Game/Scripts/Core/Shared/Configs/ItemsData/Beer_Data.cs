
using UnityEngine;

namespace Core.Shared.Items
{
    [CreateAssetMenu(fileName = "Beer", menuName = "Configs/Beer Type")]
    public class Beer_Data : Item_Data
    {
        [Space]
        [Header("Beer Data")]
        [SerializeField] private float _fillSpeed = 1f;
        [SerializeField] private FloatAreaOnRange _goodArea;
        [SerializeField] private FloatAreaOnRange _perfectArea;


        public float FillSpeed => _fillSpeed;
        public FloatAreaOnRange PerfectArea => _perfectArea;
        public FloatAreaOnRange GoodArea => _goodArea;


        private void OnValidate()
        {
            if (_perfectArea.Min < _goodArea.Min)
                _perfectArea.Min = _goodArea.Min;

            if (_perfectArea.Max > _goodArea.Max)
                _perfectArea.Max = _goodArea.Max;

        }

    }
}
