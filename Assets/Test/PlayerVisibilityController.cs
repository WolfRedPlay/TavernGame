using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityController : MonoBehaviour
{
    [SerializeField] GameObject _heroModel;
    [SerializeField] string _hiddenLayerName;
    [SerializeField] string _visibleLayerName;

    private void Awake()
    {
        if (_heroModel == null)
        {
            Debug.LogWarning("Hero model wasn't assigned to Player Visibility Controller");
            enabled = false;
            return;
        }

        if(LayerMask.NameToLayer(_hiddenLayerName) == -1)
        {
            Debug.LogWarning("Name of hidden layer for player is incorrect");
            enabled = false;
            return;
        }
        
        if(LayerMask.NameToLayer(_visibleLayerName) == -1)
        {
            Debug.LogWarning("Name of visible layer for player is incorrect");
            enabled = false;
            return;
        }
    }

    public void SetVisibility(bool visible)
    {
        if (visible)
        {
            _heroModel.layer = LayerMask.NameToLayer(_visibleLayerName);
            foreach (Transform child in _heroModel.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(_visibleLayerName);
            }
        }
        else
        {
            _heroModel.layer = LayerMask.NameToLayer(_hiddenLayerName);
            foreach (Transform child in _heroModel.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(_hiddenLayerName);
            }

        }
    }
}
