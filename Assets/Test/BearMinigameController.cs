using UnityEngine;
using UnityEngine.UI;

public class BearMinigameController : MonoBehaviour
{
    [SerializeField] BearBarrelInteractable _interactable;
    [SerializeField] Slider _progressField;
    [SerializeField] float _fillSpeed = 1f;


    bool _isHold = false;
    private void Awake()
    {
        //проверка что прогресс бар и интерактабл не равен 0
    }

    void Update()
    {
        if (_isHold)
        {
            float newValue = _progressField.value + _fillSpeed * Time.deltaTime;
            if (newValue > _progressField.maxValue) Fail();
            _progressField.value = newValue;
        }
    }

    public void StartFilling()
    {
        _isHold = true;
    }


    public void StopFilling()
    {
        _isHold = false;
        _interactable.StopInteraction();
        Debug.Log("Filling stopped!");
    }

    private void Fail()
    {
        Debug.Log("Faild!");
        StopFilling();
    }
}
