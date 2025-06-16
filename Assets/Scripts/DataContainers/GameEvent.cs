using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Events/GameEvent")]
public class GameEvent:ScriptableObject
{
    public Action onEventRaised;

    public void Raise()
    {
        onEventRaised?.Invoke();
    }
}
