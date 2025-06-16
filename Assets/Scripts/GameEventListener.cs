using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    private void OnEnable()
    {
        gameEvent.onEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        gameEvent.onEventRaised -= OnEventRaised;   
    }

    private void OnEventRaised()
    {
        Debug.Log("Event was raised!");
    }
}
