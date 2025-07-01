using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInteractable : MoveToInteractable
{
    [SerializeField] GameObject _camera;

    PlayerVisibilityController _playerVisibility;

    protected override void Awake()
    {
        base.Awake();

        _playerVisibility = FindAnyObjectByType<PlayerVisibilityController>(FindObjectsInactive.Include);
        if (_playerVisibility == null)
        {
            Debug.LogError("Player Visibility Controller wasn't found!!!");
            enabled = false;
            return;
        }

        if (_camera == null)
        {
            Debug.LogWarning("Zoom Interactable " + gameObject.name +" doesn't have the camera!");
            enabled = false;
            return;
        }
        ZoomOut();

        EventManager.AddListener<PathFinishedEvent>(ZoomIn);
    }

    private void ZoomIn(PathFinishedEvent evt)
    {
        if (evt.Interactable == this)
        {
            _camera.SetActive(true);
            _playerVisibility.SetVisibility(false);
        }
    }

    public void ZoomOut()
    {
        _camera.SetActive(false);
        _playerVisibility.SetVisibility(true);

    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PathFinishedEvent>(ZoomIn);

    }
}
