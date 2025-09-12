using UnityEngine;

namespace Gameplay 
{
    public class VisibilityController
    {
        private Transform _rootObjectTransform;
        private int _defaultLayer;


        private const string HiddenLayer = "HiddenPlayer";


        public VisibilityController(Transform rootObjectTransform)
        {
            if (rootObjectTransform == null)
                Debug.LogError("Root object in Visibility Controller is null!!!");

            _defaultLayer = rootObjectTransform.gameObject.layer;
            _rootObjectTransform = rootObjectTransform;
        }


        public void Hide()
        {
            if (_rootObjectTransform != null)
                ChangeLayerDeep(_rootObjectTransform, LayerMask.NameToLayer(HiddenLayer));
        }

        public void Show()
        {
            if (_rootObjectTransform != null)
                ChangeLayerDeep(_rootObjectTransform, _defaultLayer);
        }


        private void ChangeLayerDeep(Transform objectToChangeLayer, int newLayer)
        {
            objectToChangeLayer.gameObject.layer = newLayer;

            if (objectToChangeLayer.childCount == 0) return;

            foreach (Transform child in objectToChangeLayer.transform)
            {
                ChangeLayerDeep(child, newLayer);
            }
        }
    }
}

