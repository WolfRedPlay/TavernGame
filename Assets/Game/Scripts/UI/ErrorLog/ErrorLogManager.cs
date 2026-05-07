using Core.Events;
using UnityEngine;

namespace UI.ErrorLog
{
    public class ErrorLogManager : MonoBehaviour
    {
        [SerializeField]
        private ErrorMessage _errorMessagePrefab;


        public void Start()
        {
            EventManager.AddListener<ShowErrorMessageEvent>(OnShowErrorMessage);
        }


        private void OnShowErrorMessage(ShowErrorMessageEvent evt)
        {
            ShowErrorMessage(evt.Message);
        }


        private void ShowErrorMessage(string message)
        {
            Instantiate(_errorMessagePrefab, transform).Initialize(message);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<ShowErrorMessageEvent>(OnShowErrorMessage);
        }
    }
}