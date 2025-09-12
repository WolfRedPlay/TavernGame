namespace Core.Shared
{
    [System.Serializable]
    public class FloatAreaOnRange
    {
        public float Min = 0;
        public float Max = 0;


        public bool IsValueInArea(float value)
        {
            return (value > Min && value < Max);
        }
    }
}
