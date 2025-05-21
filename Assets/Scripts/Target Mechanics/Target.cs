using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    //An event which is accesible in the inspector and other modules
    public event Action<Target> OnDestroyed;

    //Simply on destroy invoke OnDestroyed event
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
