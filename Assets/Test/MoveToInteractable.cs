using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MoveToInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _destination;


    public Transform Destination => _destination;

    private void OnMouseDown()
    {
        Interact();
    }



    public virtual void Interact()
    {
        MoveToInteractEvent newEvent = new MoveToInteractEvent();
        newEvent.Interactable = this;
        EventManager.Broadcast(newEvent);
    }

    protected virtual void Awake()
    {
        if (_destination == null)
        {
            Debug.LogWarning("Move To Interactable " + gameObject.name + " doesn't have direction point!");
            enabled = false;
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
