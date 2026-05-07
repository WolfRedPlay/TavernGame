using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.ErrorLog
{
    public class ErrorMessage : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _messageText;
        [SerializeField]
        private CanvasGroup _canvasGroup;


        public void Initialize(string message)
        {
            _messageText.text = message;
            _canvasGroup.alpha = 1f;

            StartCoroutine(VanishAnimation(2f));
        }


        private IEnumerator VanishAnimation(float sec)
        {
            yield return new WaitForSeconds(sec);

            while(_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
            _canvasGroup.alpha = 0;

            yield return null;
            Destroy(gameObject);
        }

    }
}
