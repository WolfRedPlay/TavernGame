using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovementController : MonoBehaviour
{

    NavMeshAgent _agent;
    MoveToInteractable _target;
    bool _isBusy = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        EventManager.AddListener<MoveToInteractEvent>(OnMoveToIntractable);
    }

    private void OnMoveToIntractable(MoveToInteractEvent evt)
    {
        if (_isBusy) return;

        _target = evt.Interactable;
        _agent.destination = _target.Destination.position;

    }

    void Update()
    {
        UpdateRotation();

    }

    private void UpdateRotation()
    {
        if (_agent.hasPath) return;

        if (_target == null) return;

        transform.rotation = Quaternion.Lerp(transform.rotation, _target.Destination.rotation, _agent.angularSpeed / 50 * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, _target.Destination.rotation) <= 5)
        {
            transform.rotation = _target.Destination.rotation;
            PathFinishedEvent newEvent = new PathFinishedEvent();
            newEvent.Interactable = _target;
            EventManager.Broadcast(newEvent);
            _target = null;
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoveToInteractEvent>(OnMoveToIntractable);

    }
}
