using UnityEngine;

    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.

public static class Events
{
}

public class MoveToInteractEvent : GameEvent
{
    public MoveToInteractable Interactable;
}
public class PathFinishedEvent : GameEvent
{
    public MoveToInteractable Interactable;
}

