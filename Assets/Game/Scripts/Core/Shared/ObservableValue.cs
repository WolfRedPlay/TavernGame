using UnityEngine.Events;

namespace Core.Shared
{
    public class ObservableValue<T>
    {
        private T _value;

        public T Value 
        { 
            get => _value; 
            set
            {
                _value = value;
                OnValueChanged?.Invoke(value);
            } 
        }


        public UnityAction<T> OnValueChanged;


        public ObservableValue(T startValue = default)
        {
            Value = startValue;
        }
    }
}
