using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Shared
{
    public abstract class UI_Interactable : MonoBehaviour
    {
        [SerializeField] protected GameObject _rootObject;
        [SerializeField] protected Button _closeButton;


        public event UnityAction OnCloseButtonClick;


        public virtual void Initialize()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClick?.Invoke());

            CloseUI();
        }


        public virtual void CloseUI()
        {
            _rootObject.SetActive(false);
        }
    }
}